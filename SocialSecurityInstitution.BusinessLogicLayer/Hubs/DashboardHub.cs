using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.CommonEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.Hubs
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class DashboardHub : Hub
    {
        private readonly ILogger<DashboardHub> _logger;
        private readonly IHubConnectionService _hubConnectionService;
        private readonly IHubConnectionCustomService _hubConnectionCustomService;
        private readonly ISiralarService _siralarService;
        private readonly ISiralarCustomService _siralarCustomService;
        private readonly IUserContextService _userContextService;

        public DashboardHub(ILogger<DashboardHub> logger,
                            IHubConnectionService hubConnectionService,
                            IHubConnectionCustomService hubConnectionCustomService,
                            ISiralarService siralarService,
                            ISiralarCustomService siralarCustomService,
                            IUserContextService userContextService)
        {
            _logger = logger;
            _hubConnectionService = hubConnectionService;
            _hubConnectionCustomService = hubConnectionCustomService;
            _siralarService = siralarService;
            _siralarCustomService = siralarCustomService;
            _userContextService = userContextService;
        }

        public async Task SendUpdates(string tableName, object entity, DatabaseAction action)
        {
            _logger.LogInformation($"{tableName} tablosu için {action} işlemi yapılıyor.");
            await Clients.All.SendAsync("ReceiveUpdates", tableName, entity, action.ToString());
            _logger.LogInformation($"{action} işlemi tamamlandı.");
        }

        public async Task SaveUserConnection()
        {
            string tcKimlikNo = Context.User?.FindFirst("TcKimlikNo")?.Value;
            string adSoyad = Context.User?.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            var hizmetBinasiId = int.TryParse(Context.User?.FindFirst("HizmetBinasiId")?.Value, out var parsedHizmetBinasiId) ? parsedHizmetBinasiId : 0;

            var connectionId = Context.ConnectionId;
            _logger.LogInformation($"Kullanıcı bağlandı: {adSoyad} ({tcKimlikNo}) ConnectionId: {connectionId}");

            try
            {
                var existingConnection = await _hubConnectionCustomService.GetHubConnectionWithTcKimlikNoAsync(tcKimlikNo);

                if (existingConnection != null)
                {
                    // Mevcut kaydı güncelliyorum
                    existingConnection.ConnectionId = connectionId;
                    existingConnection.ConnectionStatus = ConnectionStatus.online;
                    existingConnection.IslemZamani = DateTime.Now;

                    var updateConnectionResult = await _hubConnectionService.TUpdateAsync(existingConnection);

                    if (updateConnectionResult)
                    {
                        _logger.LogInformation("Kullanıcı ConnectionId ve ConnectionStatus güncellendi.");
                    }
                    else
                    {
                        _logger.LogWarning("Kullanıcı ConnectionId güncellenemedi!");
                    }
                }
                else
                {
                    // Yeni kayıt ekliyoruz
                    var newConnection = new HubConnectionDto
                    {
                        TcKimlikNo = tcKimlikNo,
                        ConnectionId = connectionId,
                        ConnectionStatus = ConnectionStatus.online,
                        IslemZamani = DateTime.Now
                    };

                    var insertResult = await _hubConnectionService.TInsertAsync(newConnection);
                    if (insertResult.IsSuccess)
                    {
                        _logger.LogInformation("Kullanıcı ConnectionId kaydedildi.");
                        await SendUpdates("HubConnections", newConnection, DatabaseAction.INSERT);
                    }
                    else
                    {
                        _logger.LogWarning("Kullanıcı ConnectionId kaydedilemedi!");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"TcKimlikNo: {tcKimlikNo}, ConnectionId: {connectionId} için kullanıcı bağlantısı kaydedilirken hata oluştu.");
                await Clients.Caller.SendAsync("ReceiveError", "Bağlantı kaydedilirken bir hata oluştu. Lütfen tekrar deneyin.");
                throw;
            }
        }

        public async Task UpdateUserConnectionStatus(ConnectionStatus status)
        {
            string tcKimlikNo = Context.User?.FindFirst("TcKimlikNo")?.Value;
            string adSoyad = Context.User?.FindFirst(JwtRegisteredClaimNames.Name)?.Value;

            try
            {
                var existingConnection = await _hubConnectionCustomService.GetHubConnectionWithTcKimlikNoAsync(tcKimlikNo);

                if (existingConnection != null)
                {
                    existingConnection.ConnectionStatus = status;
                    existingConnection.IslemZamani = DateTime.Now;

                    var updateResult = await _hubConnectionService.TUpdateAsync(existingConnection);

                    if (updateResult)
                    {
                        _logger.LogInformation($"Kullanıcı ConnectionStatus güncellendi: {tcKimlikNo} -> {status}");
                    }
                    else
                    {
                        _logger.LogWarning($"Kullanıcı ConnectionStatus güncellenemedi: {tcKimlikNo}");
                    }
                }
                else
                {
                    _logger.LogWarning($"Kullanıcı ConnectionStatus güncellenemedi, çünkü kayıt bulunamadı: {tcKimlikNo}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"TcKimlikNo: {tcKimlikNo} için ConnectionStatus güncellenirken hata oluştu.");
                throw;
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var connection = await _hubConnectionCustomService.GetHubConnectionWithConnectionIdAsync(connectionId);

            if (connection != null)
            {
                connection.ConnectionStatus = ConnectionStatus.offline;
                await _hubConnectionService.TUpdateAsync(connection);
                _logger.LogInformation($"ConnectionId: {connectionId} olan kullanıcı bağlantısı kesildi ve durumu güncellendi.");
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task PingServer()
        {
            var auth = Context.User?.Identity?.IsAuthenticated ?? false;
            if (!auth)
            {
                await Clients.Caller.SendAsync("SessionExpired");
            }
        }

        public async Task SendNotificationToUser(string message)
        {
            string tcKimlikNo = Context.User?.FindFirst("TcKimlikNo")?.Value;
            string adSoyad = Context.User?.FindFirst(JwtRegisteredClaimNames.Name)?.Value;

            try
            {
                var connectionInfo = await _hubConnectionCustomService.GetHubConnectionWithTcKimlikNoAsync(tcKimlikNo);

                if (connectionInfo != null)
                {
                    var connectionId = connectionInfo.ConnectionId;
                    await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
                    _logger.LogInformation($"Bildirim gönderildi: {tcKimlikNo} (ConnectionId: {connectionId})");
                }
                else
                {
                    _logger.LogWarning($"TcKimlikNo {tcKimlikNo} için bağlantı bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"TcKimlikNo {tcKimlikNo} için bildirim gönderilirken hata oluştu.");
                throw;
            }
        }

        public async Task SendNotificationToOthers(string connectionId, string message, string title = null)
        {
            try
            {
                await Clients.AllExcept(connectionId).SendAsync("ReceiveNotification", message, title);
                _logger.LogInformation($"Bildirim ConnectionId: {connectionId} dışındaki herkese gönderildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ConnectionId: {connectionId} dışındaki kullanıcılara bildirim gönderilirken hata oluştu.");
                throw;
            }
        }

        public async Task GetSiraCagirma()
        {
            string tcKimlikNo = Context.User?.FindFirst("TcKimlikNo")?.Value;
            string adSoyad = Context.User?.FindFirst(JwtRegisteredClaimNames.Name)?.Value;

            try
            {
                var siraCagirmaDto = await _siralarCustomService.GetSiraCagirmaAsync(tcKimlikNo);

                if (siraCagirmaDto != null)
                {
                    await Clients.Caller.SendAsync("ReceiveSiraCagirmaBilgisi", siraCagirmaDto);
                    _logger.LogInformation($"{tcKimlikNo} için sıra bilgisi gönderildi.");
                }
                else
                {
                    await Clients.Caller.SendAsync("ReceiveError", $"{tcKimlikNo} için sıra bilgisi bulunamadı.");
                    _logger.LogWarning($"{tcKimlikNo} için sıra bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Sıra çağırma işlemi sırasında hata oluştu: {tcKimlikNo}");
                await Clients.Caller.SendAsync("ReceiveError", "Sıra çağırma işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin.");
            }
        }
    }
}
