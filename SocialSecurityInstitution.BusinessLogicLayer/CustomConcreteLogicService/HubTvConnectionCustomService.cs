using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class HubTvConnectionCustomService : IHubTvConnectionCustomService
    {
        private readonly IHubTvConnectionDal _hubTvConnectionDal;
        private readonly ILogger<HubTvConnectionCustomService> _logger;

        public HubTvConnectionCustomService(
            IHubTvConnectionDal hubTvConnectionDal,
            ILogger<HubTvConnectionCustomService> logger)
        {
            _hubTvConnectionDal = hubTvConnectionDal;
            _logger = logger;
        }

        public async Task<HubTvConnectionDto> GetHubTvConnectionWithConnectionIdAsync(string connectionId)
        {
            try
            {
                // Business validation
                if (string.IsNullOrWhiteSpace(connectionId))
                {
                    _logger.LogWarning("Invalid connectionId provided: empty or null");
                    return null;
                }

                // Repository'den DTO al
                var result = await _hubTvConnectionDal.GetHubTvConnectionByConnectionIdAsync(connectionId);

                if (result == null)
                {
                    _logger.LogWarning("ConnectionId {ConnectionId} ile eşleşen kayıt bulunamadı", connectionId);
                }
                else
                {
                    _logger.LogInformation("Hub TV connection retrieved for ConnectionId: {ConnectionId}", connectionId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hub TV connection for ConnectionId: {ConnectionId}", connectionId);
                throw;
            }
        }

        public async Task<HubTvConnectionDto> GetHubTvConnectionWithTvIdAsync(int tvId)
        {
            try
            {
                // Business validation
                if (tvId <= 0)
                {
                    _logger.LogWarning("Invalid tvId provided: {TvId}", tvId);
                    return null;
                }

                // Repository'den DTO al
                var result = await _hubTvConnectionDal.GetHubTvConnectionByTvIdAsync(tvId);

                if (result == null)
                {
                    _logger.LogWarning("TvId {TvId} ile eşleşen kayıt bulunamadı", tvId);
                }
                else
                {
                    _logger.LogInformation("Hub TV connection retrieved for TvId: {TvId}", tvId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hub TV connection for TvId: {TvId}", tvId);
                throw;
            }
        }
    }
}
