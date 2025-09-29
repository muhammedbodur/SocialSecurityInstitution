using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class HubConnectionCustomService : IHubConnectionCustomService
    {
        private readonly IHubConnectionDal _hubConnectionDal;
        private readonly ILogger<HubConnectionCustomService> _logger;

        public HubConnectionCustomService(
            IHubConnectionDal hubConnectionDal,
            ILogger<HubConnectionCustomService> logger)
        {
            _hubConnectionDal = hubConnectionDal;
            _logger = logger;
        }

        public async Task<HubConnectionDto> GetHubConnectionWithConnectionIdAsync(string connectionId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(connectionId))
                {
                    _logger.LogWarning("Invalid connectionId provided");
                    return null;
                }

                var result = await _hubConnectionDal.GetHubConnectionByConnectionIdAsync(connectionId);

                if (result == null)
                {
                    _logger.LogWarning("ConnectionId {ConnectionId} ile eşleşen kayıt bulunamadı", connectionId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hub connection for connectionId: {ConnectionId}", connectionId);
                throw;
            }
        }

        public async Task<HubConnectionDto> GetHubConnectionWithTcKimlikNoAsync(string tcKimlikNo)
        {
            try
            {
                if (!IsValidTcKimlikNo(tcKimlikNo))
                {
                    _logger.LogWarning("Invalid TC Kimlik No format: {TcKimlikNo}", tcKimlikNo);
                    return null;
                }

                var result = await _hubConnectionDal.GetHubConnectionByTcKimlikNoAsync(tcKimlikNo);

                if (result == null)
                {
                    _logger.LogWarning("TcKimlikNo {TcKimlikNo} ile eşleşen kayıt bulunamadı", tcKimlikNo);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hub connection for TC: {TcKimlikNo}", tcKimlikNo);
                throw;
            }
        }

        public async Task<List<HubConnectionDto>> GetActiveConnectionsAsync()
        {
            try
            {
                var result = await _hubConnectionDal.GetActiveConnectionsAsync();

                var recentConnections = FilterRecentConnections(result);

                _logger.LogInformation("Retrieved {TotalCount} active connections, {RecentCount} recent",
                                     result.Count, recentConnections.Count);

                return recentConnections;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active hub connections");
                throw;
            }
        }

        public async Task<List<HubConnectionDto>> GetConnectionsByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            try
            {
                if (hizmetBinasiId <= 0)
                {
                    _logger.LogWarning("Invalid hizmetBinasiId: {HizmetBinasiId}", hizmetBinasiId);
                    return new List<HubConnectionDto>();
                }

                var result = await _hubConnectionDal.GetConnectionsByHizmetBinasiIdAsync(hizmetBinasiId);

                _logger.LogInformation("Retrieved {Count} connections for hizmet binası: {HizmetBinasiId}",
                                     result.Count, hizmetBinasiId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving connections for hizmet binası: {HizmetBinasiId}", hizmetBinasiId);
                throw;
            }
        }

        public async Task<bool> ValidateConnectionAsync(string connectionId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(connectionId))
                    return false;

                var isActive = await _hubConnectionDal.IsConnectionActiveAsync(connectionId);

                _logger.LogInformation("Connection validation - ID: {ConnectionId}, IsActive: {IsActive}",
                                     connectionId, isActive);

                return isActive;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating connection: {ConnectionId}", connectionId);
                return false;
            }
        }

        public async Task<bool> UpdateConnectionStatusAsync(string connectionId, ConnectionStatus status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(connectionId))
                {
                    _logger.LogWarning("Invalid connectionId for status update");
                    return false;
                }

                if (!IsValidStatus(status))
                {
                    _logger.LogWarning("Invalid status: {Status}", status);
                    return false;
                }

                var updated = await _hubConnectionDal.UpdateConnectionStatusAsync(connectionId, status);

                if (updated)
                {
                    _logger.LogInformation("Connection status updated: {ConnectionId} to {Status}",
                                         connectionId, status);
                }

                return updated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating connection status: {ConnectionId} to {Status}",
                               connectionId, status);
                return false;
            }
        }

        public async Task<Dictionary<ConnectionStatus, int>> GetConnectionStatisticsAsync()
        {
            try
            {
                var stats = await _hubConnectionDal.GetConnectionStatisticsAsync();

                _logger.LogInformation("Connection statistics: Online={Online}, Offline={Offline}",
                                     stats.GetValueOrDefault(ConnectionStatus.online, 0),
                                     stats.GetValueOrDefault(ConnectionStatus.offline, 0));

                return stats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving connection statistics");
                return new Dictionary<ConnectionStatus, int>();
            }
        }

        public async Task<List<HubConnectionDto>> GetRecentConnectionsAsync(int hours = 24)
        {
            try
            {
                if (hours <= 0 || hours > 168) // Max 1 week
                {
                    _logger.LogWarning("Invalid hours parameter: {Hours}", hours);
                    hours = 24;
                }

                var result = await _hubConnectionDal.GetRecentConnectionsAsync(hours);

                _logger.LogInformation("Retrieved {Count} connections from last {Hours} hours",
                                     result.Count, hours);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recent connections");
                throw;
            }
        }

        public async Task<bool> IsPersonelOnlineAsync(string tcKimlikNo)
        {
            try
            {
                if (!IsValidTcKimlikNo(tcKimlikNo))
                    return false;

                var isOnline = await _hubConnectionDal.HasActiveConnectionAsync(tcKimlikNo);

                _logger.LogInformation("Personel online check - TC: {TcKimlikNo}, IsOnline: {IsOnline}",
                                     tcKimlikNo, isOnline);

                return isOnline;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if personel online: {TcKimlikNo}", tcKimlikNo);
                return false;
            }
        }

        private bool IsValidTcKimlikNo(string tcKimlikNo)
        {
            return !string.IsNullOrWhiteSpace(tcKimlikNo) &&
                   tcKimlikNo.Length == 11 &&
                   tcKimlikNo.All(char.IsDigit) &&
                   tcKimlikNo != "00000000000" &&
                   tcKimlikNo != "11111111111";
        }

        private bool IsValidStatus(ConnectionStatus status)
        {
            return status == ConnectionStatus.online || status == ConnectionStatus.offline;
        }

        private List<HubConnectionDto> FilterRecentConnections(List<HubConnectionDto> connections)
        {
            var recentThreshold = DateTime.Now.AddMinutes(-30);
            return connections.Where(c => c.IslemZamani >= recentThreshold).ToList();
        }
    }
}