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
    private readonly IHubContext<TvHub> _hubTvContext;
    private readonly ILogger<SubscribeTableDependency<T>> _logger;
    private SqlTableDependency<T>? _sqlTableDependency;
    private readonly IMapper _mapper;

    public SubscribeTableDependency(IServiceProvider serviceProvider, IHubContext<DashboardHub> hubContext, IHubContext<TvHub> hubTvContext, ILogger<SubscribeTableDependency<T>> logger, IMapper mapper)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
        _hubTvContext = hubTvContext;
        _logger = logger;
        _mapper = mapper;
    }

    public void SubscribeTablesDependency(string connectionString)
    {
        try
        {
            string tableName = "SM_" + typeof(T).Name;

            _logger.LogInformation($"TableDependency başlatılıyor: {tableName}");

            _sqlTableDependency = new SqlTableDependency<T>(connectionString, tableName);
            _sqlTableDependency.OnChanged += TableDependency_OnChanged;
            _sqlTableDependency.OnError += TableDependency_OnError;
            _sqlTableDependency.Start();

            _logger.LogInformation($"TableDependency başarıyla başlatıldı: {tableName}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"TableDependency başlatılırken hata oluştu: {typeof(T).Name}");
            // Hata durumunda uygulama çökmemesi için exception'ı yakalıyoruz
        }
    }

    private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<T> e)
    {
        if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
        {
            _logger.LogInformation($"{typeof(T).Name} data changed: {e.ChangeType}");
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
                        try
                        {
                            var siralarService = scope.ServiceProvider.GetRequiredService<ISiralarService>();
                            var siralarCustomService = scope.ServiceProvider.GetRequiredService<ISiralarCustomService>();
                            var personelCustomService = scope.ServiceProvider.GetRequiredService<IPersonelCustomService>();
                            var kanalPersonelleriCustomService = scope.ServiceProvider.GetRequiredService<IKanalPersonelleriCustomService>();
                            var tvlerCustomService = scope.ServiceProvider.GetRequiredService<ITvlerCustomService>();
                            var bankolarKullaniciCustomService = scope.ServiceProvider.GetRequiredService<IBankolarKullaniciCustomService>();

                            await HandleSiralarTableChange(siraEntity, action, siralarService, personelCustomService, siralarCustomService, kanalPersonelleriCustomService, tvlerCustomService, bankolarKullaniciCustomService);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Siralar table change handling sırasında hata oluştu");
                        }
                    }
                }
            }
            else
            {
                // Diğer tablolar için genel işlem
                try
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveUpdates", typeof(T).Name, e.Entity, action.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"SignalR mesajı gönderilirken hata: {typeof(T).Name}");
                }
            }
        }
    }

    private async Task HandleSiralarTableChange(Siralar siraEntity, Enums.DatabaseAction action, ISiralarService siralarService, IPersonelCustomService personelCustomService, ISiralarCustomService siralarCustomService, IKanalPersonelleriCustomService kanalPersonelleriCustomService, ITvlerCustomService tvlerCustomService, IBankolarKullaniciCustomService bankolarKullaniciCustomService)
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

                    if (uzmanlik != PersonelUzmanlik.BilgisiYok && (sira.BeklemeDurum == BeklemeDurum.Beklemede ||
                        (sira.BeklemeDurum == BeklemeDurum.Cagrildi && sira.TcKimlikNo == personel.TcKimlikNo)))
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

                if (!string.IsNullOrEmpty(personel.ConnectionId))
                {
                    connectionGroups[personel.ConnectionId] = kullaniciSiralar;
                }
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
                    _logger.LogError(ex, $"Sira verisi gönderilemedi - ConnectionId: {group.Key}, SiraId: {siraEntity.SiraId}");
                }
            }

            /* TV lere Sıra Bilgileri Gönderilmekte */
            if (siraEntity.BeklemeDurum == BeklemeDurum.Cagrildi)
            {
                try
                {
                    /*
                    -Sıra Çağrıldığında önce TcKimlikNo dan çağırak kullanıcının hangi BankoId ye sahip olduğu bilgisi
                    alıncak, ardından o BankoId hangi TvId lere ait olduğu alınıp sadece onlara gönderilecek.
                    
                    -Gönderilecek Sıralar ise o TV de görünecek olanlar olmalı
                    */
                    var tvler = await tvlerCustomService.GetTvlerConnectionWithBankolarKullaniciTcKimlikNo(siraEntity.TcKimlikNo);

                    foreach (var tv in tvler)
                    {
                        try
                        {
                            var tvSiralar = await siralarCustomService.GetSiralarForTvWithTvId(tv.TvId);
                            if (tv != null && !string.IsNullOrEmpty(tv.ConnectionId))
                            {
                                await _hubTvContext.Clients.Client(tv.ConnectionId).SendAsync("ReceiveTvSiraBilgisi", tvSiralar, action.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"TV'ye sira verisi gönderilemedi - TvId: {tv.TvId}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "TV'lere sira bilgisi gönderilirken hata oluştu");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HandleSiralarTableChange metodunda hata oluştu.");

            try
            {
                var personellerForError = await personelCustomService.GetPersonellerWithHizmetBinasiIdAsync(siraEntity.HizmetBinasiId);
                foreach (var personel in personellerForError)
                {
                    if (!string.IsNullOrEmpty(personel.ConnectionId))
                    {
                        await _hubContext.Clients.Client(personel.ConnectionId).SendAsync("ReceiveError", "Sıralar güncellenirken bir hata oluştu.");
                    }
                }
            }
            catch (Exception errorEx)
            {
                _logger.LogError(errorEx, "Error mesajı gönderilirken bile hata oluştu");
            }
        }
    }

    private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
    {
        _logger.LogError($"{typeof(T).Name} SQL table dependency error: {e.Error.Message}");
    }

    // IDisposable pattern for cleanup
    public void Dispose()
    {
        try
        {
            _sqlTableDependency?.Stop();
            _sqlTableDependency?.Dispose();
            _logger.LogInformation($"TableDependency disposed: {typeof(T).Name}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"TableDependency dispose edilirken hata: {typeof(T).Name}");
        }
    }
}