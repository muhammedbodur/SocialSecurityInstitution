using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.Hubs;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.SqlDependencyServices;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using AutoMapper;

public class SubscribeTableDependency<T> : ISubscribeTableDependency where T : class, new()
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<DashboardHub> _hubContext;
    private readonly ILogger<SubscribeTableDependency<T>> _logger;
    private SqlTableDependency<T>? _sqlTableDependency;
    private readonly IMapper _mapper;

    public SubscribeTableDependency(IServiceProvider serviceProvider, IHubContext<DashboardHub> hubContext, ILogger<SubscribeTableDependency<T>> logger, IMapper mapper)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
        _logger = logger;
        _mapper = mapper;
    }

    public void SubscribeTablesDependency(string connectionString)
    {
        _sqlTableDependency = new SqlTableDependency<T>(connectionString);
        _sqlTableDependency.OnChanged += TableDependency_OnChanged;
        _sqlTableDependency.OnError += TableDependency_OnError;
        _sqlTableDependency.Start();
    }

    private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<T> e)
    {
        if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
        {
            _logger.LogInformation($"{typeof(T).Name} data: {e.Entity}");
            Enums.DatabaseAction action;

            switch (e.ChangeType)
            {
                case TableDependency.SqlClient.Base.Enums.ChangeType.Insert:
                    action = Enums.DatabaseAction.INSERT;
                    break;
                case TableDependency.SqlClient.Base.Enums.ChangeType.Update:
                    action = Enums.DatabaseAction.UPDATE;
                    break;
                case TableDependency.SqlClient.Base.Enums.ChangeType.Delete:
                    action = Enums.DatabaseAction.DELETE;
                    break;
                default:
                    action = Enums.DatabaseAction.NONE;
                    break;
            }

            if (typeof(T) == typeof(Siralar))
            {
                var siraEntity = e.Entity as Siralar;
                if (siraEntity != null && siraEntity.BeklemeDurum != BeklemeDurum.Bitti)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var siralarService = scope.ServiceProvider.GetRequiredService<ISiralarService>();
                        var siralarCustomService = scope.ServiceProvider.GetRequiredService<ISiralarCustomService>();
                        var personelCustomService = scope.ServiceProvider.GetRequiredService<IPersonelCustomService>();
                        var kanalPersonelleriCustomService = scope.ServiceProvider.GetRequiredService<IKanalPersonelleriCustomService>();
                        await HandleSiralarTableChange(siraEntity, action, siralarService, personelCustomService, siralarCustomService, kanalPersonelleriCustomService);
                    }
                }
            }
            else
            {
                // Diğer tablolar için genel işlem
                // await _hubContext.Clients.All.SendAsync("ReceiveUpdates", typeof(T).Name, e.Entity, action.ToString());
            }
        }
    }

    private async Task HandleSiralarTableChange(Siralar siraEntity, Enums.DatabaseAction action, ISiralarService siralarService, IPersonelCustomService personelCustomService, ISiralarCustomService siralarCustomService, IKanalPersonelleriCustomService kanalPersonelleriCustomService)
    {
        if (siraEntity == null)
        {
            _logger.LogError("Sira entity null olarak geldi!");
            return;
        }

        try
        {
            int hizmetBinasiId = siraEntity.HizmetBinasiId;

            var personeller = await personelCustomService.GetPersonellerWithHizmetBinasiIdAsync(hizmetBinasiId);
            var siralar = await siralarCustomService.GetSiralarWithHizmetBinasiAsync(hizmetBinasiId);
            var kanalPersonelleri = await kanalPersonelleriCustomService.GetKanalPersonelleriWithHizmetBinasiIdAsync(hizmetBinasiId);

            // Kullanıcılar için connectionId'ye göre gruplama yapıyorum
            var connectionGroups = new Dictionary<string, List<siraCagirmaDto>>();
            
            foreach (var personel in personeller)
            {
                var kullaniciSiralar = new List<siraCagirmaDto>();

                foreach (var sira in siralar)
                {
                    var uzmanlik = kanalPersonelleri.FirstOrDefault(kp => kp.TcKimlikNo == personel.TcKimlikNo && kp.KanalAltIslemId == sira.KanalAltIslemId)?.Uzmanlik ?? PersonelUzmanlik.BilgisiYok;

                    if (sira.BeklemeDurum == BeklemeDurum.Beklemede ||
                        (sira.BeklemeDurum == BeklemeDurum.Cagrildi && sira.TcKimlikNo == personel.TcKimlikNo))
                    {
                        string islemiYapan = sira.TcKimlikNo == personel.TcKimlikNo ? "kendisi" : "baskasi";

                        kullaniciSiralar.Add(new siraCagirmaDto
                        {
                            SiraId = sira.SiraId,
                            SiraNo = sira.SiraNo,
                            BeklemeDurum = sira.BeklemeDurum,
                            TcKimlikNo = sira.TcKimlikNo,
                            KanalAltAdi = sira.KanalAltAdi,
                            IslemiYapan = islemiYapan,
                            PersonelUzmanlik = uzmanlik
                        });
                    }
                }

                // Uzmanlık seviyesine göre sıralama yapıyorum
                kullaniciSiralar = kullaniciSiralar
                    .OrderByDescending(s => s.BeklemeDurum == BeklemeDurum.Cagrildi) // BeklemeDurum = 1 olanlar en yukarıda olacak ki personelin yaptığı işlem o
                    .ThenBy(s => s.PersonelUzmanlik) // Uzmanlık seviyesine göre sıralama
                    .ThenBy(s => s.SiraNo) // Sıra numarasına göre sıralama
                    .ToList();


                connectionGroups[personel.ConnectionId] = kullaniciSiralar;
            }

            // Grupları kullanıcılara gönderiyorum
            foreach (var group in connectionGroups)
            {
                try
                {
                    var connectionId = group.Key;
                    var siraData = group.Value;
                    await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveSiraBilgisi", siraData, action.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Sira verisi gönderilemedi: {siraEntity.SiraId}");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HandleSiralarTableChange metodunda hata oluştu.");
            foreach (var personel in await personelCustomService.GetPersonellerWithHizmetBinasiIdAsync(siraEntity.HizmetBinasiId))
            {
                await _hubContext.Clients.Client(personel.ConnectionId).SendAsync("ReceiveError", "Sıralar güncellenirken bir hata oluştu.");
            }
        }
    }

    private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
    {
        _logger.LogError($"{typeof(T).Name} SQL table dependency error: {e.Error.Message}");
    }
}
