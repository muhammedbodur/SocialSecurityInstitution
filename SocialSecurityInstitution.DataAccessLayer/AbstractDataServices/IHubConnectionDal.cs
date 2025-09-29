using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IHubConnectionDal : IGenericDal<HubConnectionDto>
    {
        Task<HubConnectionDto> GetHubConnectionByConnectionIdAsync(string connectionId);
        Task<HubConnectionDto> GetHubConnectionByTcKimlikNoAsync(string tcKimlikNo);

        Task<List<HubConnectionDto>> GetActiveConnectionsAsync();
        Task<List<HubConnectionDto>> GetConnectionsByStatusAsync(ConnectionStatus status);
        Task<List<HubConnectionDto>> GetConnectionsByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<List<HubConnectionDto>> GetRecentConnectionsAsync(int hours = 24);

        Task<List<HubConnectionDto>> GetOldOfflineConnectionsAsync(DateTime cutoffTime);
        Task<bool> IsConnectionActiveAsync(string connectionId);
        Task<bool> HasActiveConnectionAsync(string tcKimlikNo);
        Task<bool> UpdateConnectionStatusAsync(string connectionId, ConnectionStatus status);
        Task<Dictionary<ConnectionStatus, int>> GetConnectionStatisticsAsync();
    }
}