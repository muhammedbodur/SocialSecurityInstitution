using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IHubConnectionCustomService
    {
        Task<HubConnectionDto> GetHubConnectionWithConnectionIdAsync(string connectionId);
        Task<HubConnectionDto> GetHubConnectionWithTcKimlikNoAsync(string tcKimlikNo);

        Task<List<HubConnectionDto>> GetActiveConnectionsAsync();
        Task<List<HubConnectionDto>> GetConnectionsByHizmetBinasiIdAsync(int hizmetBinasiId);
        Task<List<HubConnectionDto>> GetRecentConnectionsAsync(int hours = 24);
        Task<bool> ValidateConnectionAsync(string connectionId);
        Task<bool> UpdateConnectionStatusAsync(string connectionId, ConnectionStatus status);
        Task<bool> IsPersonelOnlineAsync(string tcKimlikNo);
        Task<Dictionary<ConnectionStatus, int>> GetConnectionStatisticsAsync();
    }
}