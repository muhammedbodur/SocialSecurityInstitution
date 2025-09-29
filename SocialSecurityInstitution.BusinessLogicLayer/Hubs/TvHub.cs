using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using Microsoft.Build.Framework;

namespace SocialSecurityInstitution.BusinessLogicLayer.Hubs
{
    public class TvHub : Hub
    {
        private readonly ITvlerService _tvlerService;
        private readonly ILogger<TvHub> _logger;
        private readonly IHubTvConnectionService _hubTvConnectionService;
        private readonly IHubTvConnectionCustomService _hubTvConnectionCustomService;

        public TvHub(ITvlerService tvlerService, ILogger<TvHub> logger, IHubTvConnectionService hubTvConnectionService, IHubTvConnectionCustomService hubTvConnectionCustomService)
        {
            _tvlerService = tvlerService;
            _logger = logger;
            _hubTvConnectionService = hubTvConnectionService;
            _hubTvConnectionCustomService = hubTvConnectionCustomService;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                // access_token parametresini URL'den alıyoruz (burada tvId olarak gönderiliyor)
                var tvIdParam = Context.GetHttpContext().Request.Query["access_token"].ToString();

                if (string.IsNullOrEmpty(tvIdParam))
                {
                    await Clients.Caller.SendAsync("Error", "Bağlantı reddedildi: TvId parametresi bulunamadı.");
                    Context.Abort(); // Bağlantıyı kesiyoruz
                }
                else
                {
                    if (int.TryParse(tvIdParam, out int tvId))
                    {
                        var tvlerDto = await _tvlerService.TGetByIdAsync(tvId);

                        if (tvlerDto != null)
                        {
                            await base.OnConnectedAsync();
                        }
                        else
                        {
                            // TvId geçerli ancak böyle bir TV yok
                            await Clients.Caller.SendAsync("Error", "Bağlantı reddedildi: Geçersiz TvId.");
                            Context.Abort(); // Bağlantıyı kesiyoruz
                        }
                    }
                    else
                    {
                        // TvId int bir değer değil, bağlantıyı kesiyoruz
                        await Clients.Caller.SendAsync("Error", "Bağlantı reddedildi: Geçersiz TvId.");
                        Context.Abort(); // Bağlantıyı kesiyoruz
                    }
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluştuğunda hata mesajı gönderiyoruz ve bağlantıyı kesiyoruz
                await Clients.Caller.SendAsync("Error", "Bağlantı sırasında bir hata oluştu.");
                _logger.LogError(ex, "OnConnectedAsync sırasında bir hata oluştu.");
                Context.Abort(); // Bağlantıyı kesiyoruz
            }
        }

        public async Task SaveTvConnection()
        {
            try
            {
                var tvIdParam = Context.GetHttpContext().Request.Query["access_token"].ToString();

                var connectionId = Context.ConnectionId;

                if (!string.IsNullOrEmpty(tvIdParam) && int.TryParse(tvIdParam, out int tvId))
                {
                    try
                    {
                        var existingConnection = await _hubTvConnectionCustomService.GetHubTvConnectionWithTvIdAsync(tvId);

                        if (existingConnection != null)
                        {
                            // Mevcut kaydı güncelliyorum
                            existingConnection.ConnectionId = connectionId;
                            existingConnection.ConnectionStatus = ConnectionStatus.online;
                            existingConnection.IslemZamani = DateTime.Now;

                            var updateConnectionResult = await _hubTvConnectionService.TUpdateAsync(existingConnection);

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
                            var newConnection = new HubTvConnectionDto
                            {
                                TvId = tvId,
                                ConnectionId = connectionId,
                                ConnectionStatus = ConnectionStatus.online,
                                IslemZamani = DateTime.Now
                            };

                            var insertResult = await _hubTvConnectionService.TInsertAsync(newConnection);
                            if (insertResult.IsSuccess)
                            {
                                _logger.LogInformation("TV için ConnectionId kaydedildi.");
                                await SendUpdates("HubTvConnections", newConnection, DatabaseAction.INSERT);
                            }
                            else
                            {
                                _logger.LogWarning("TV için ConnectionId kaydedilemedi!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"TvId: {tvId}, ConnectionId: {connectionId} için TV bağlantısı kaydedilirken hata oluştu.");
                        await Clients.Caller.SendAsync("ReceiveError", "Bağlantı kaydedilirken bir hata oluştu. Lütfen tekrar deneyin.");
                        throw;
                    }
                }
                else
                {
                    _logger.LogWarning("Bağlantı reddedildi: Geçersiz veya eksik TvId parametresi.");
                    await Clients.Caller.SendAsync("ReceiveError", "Bağlantı reddedildi: Geçersiz veya eksik TvId parametresi.");
                    Context.Abort();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveTvConnection sırasında beklenmedik bir hata oluştu.");
                throw;
            }
        }

        public async Task SendUpdates(string tableName, object entity, DatabaseAction action)
        {
            _logger.LogInformation($"{tableName} tablosu için {action} işlemi yapılıyor.");
            await Clients.All.SendAsync("ReceiveTvUpdates", tableName, entity, action.ToString());
            _logger.LogInformation($"{action} işlemi tamamlandı.");
        }

        public async Task PingServer()
        {
            await Clients.Caller.SendAsync("Pong");
        }
    }
}
