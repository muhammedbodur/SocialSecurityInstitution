using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class HubConnectionDal : GenericRepository<HubConnection, HubConnectionDto>, IHubConnectionDal
    {
        private readonly ILogger<HubConnectionDal> _logger;

        public HubConnectionDal(Context context, IMapper mapper, ILogService logService, ILogger<HubConnectionDal> logger)
            : base(context, mapper, logService)
        {
            _logger = logger;
        }

        public async Task<HubConnectionDto> GetHubConnectionByConnectionIdAsync(string connectionId)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
                return new HubConnectionDto();

            var entity = await _context.HubConnection
                .AsNoTracking()
                .FirstOrDefaultAsync(hc => hc.ConnectionId == connectionId);

            return _mapper.Map<HubConnectionDto>(entity);
        }

        public async Task<HubConnectionDto> GetHubConnectionByTcKimlikNoAsync(string tcKimlikNo)
        {
            if (string.IsNullOrWhiteSpace(tcKimlikNo))
                return new HubConnectionDto(); // Return empty DTO instead of null

            var entity = await _context.HubConnection
                .AsNoTracking()
                .FirstOrDefaultAsync(hc => hc.TcKimlikNo == tcKimlikNo);

            return _mapper.Map<HubConnectionDto>(entity);
        }

        public async Task<List<HubConnectionDto>> GetActiveConnectionsAsync()
        {
            var entities = await _context.HubConnection
                .Where(hc => hc.ConnectionStatus == ConnectionStatus.online)
                .OrderByDescending(hc => hc.IslemZamani)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<HubConnectionDto>>(entities);
        }

        public async Task<List<HubConnectionDto>> GetConnectionsByStatusAsync(ConnectionStatus status)
        {
            var entities = await _context.HubConnection
                .Where(hc => hc.ConnectionStatus == status)
                .OrderByDescending(hc => hc.IslemZamani)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<HubConnectionDto>>(entities);
        }

        public async Task<List<HubConnectionDto>> GetConnectionsByHizmetBinasiIdAsync(int hizmetBinasiId)
        {
            var entities = await _context.HubConnection
                .Join(_context.Personeller,
                    hc => hc.TcKimlikNo,
                    p => p.TcKimlikNo,
                    (hc, p) => new { HubConnection = hc, Personel = p })
                .Where(joined => joined.Personel.HizmetBinasiId == hizmetBinasiId &&
                               joined.HubConnection.ConnectionStatus == ConnectionStatus.online)
                .Select(joined => joined.HubConnection)
                .OrderByDescending(hc => hc.IslemZamani)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<HubConnectionDto>>(entities);
        }

        public async Task<List<HubConnectionDto>> GetRecentConnectionsAsync(int hours = 24)
        {
            var cutoffTime = DateTime.Now.AddHours(-hours);

            var entities = await _context.HubConnection
                .Where(hc => hc.IslemZamani >= cutoffTime)
                .OrderByDescending(hc => hc.IslemZamani)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<HubConnectionDto>>(entities);
        }

        public async Task<List<HubConnectionDto>> GetOldOfflineConnectionsAsync(DateTime cutoffTime)
        {
            var entities = await _context.HubConnection
                .Where(hc => hc.IslemZamani < cutoffTime &&
                           hc.ConnectionStatus == ConnectionStatus.offline)
                .OrderBy(hc => hc.IslemZamani)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<HubConnectionDto>>(entities);
        }

        public async Task<bool> IsConnectionActiveAsync(string connectionId)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
                return false;

            return await _context.HubConnection
                .AnyAsync(hc => hc.ConnectionId == connectionId &&
                              hc.ConnectionStatus == ConnectionStatus.online);
        }

        public async Task<bool> UpdateConnectionStatusAsync(string connectionId, ConnectionStatus status)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
                return false;

            var connection = await _context.HubConnection
                .FirstOrDefaultAsync(hc => hc.ConnectionId == connectionId);

            if (connection == null)
                return false;

            connection.ConnectionStatus = status;
            connection.IslemZamani = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Dictionary<ConnectionStatus, int>> GetConnectionStatisticsAsync()
        {
            var stats = await _context.HubConnection
                .GroupBy(hc => hc.ConnectionStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);

            return stats;
        }

        public async Task<bool> HasActiveConnectionAsync(string tcKimlikNo)
        {
            return await _context.HubConnection
                .AnyAsync(hc => hc.TcKimlikNo == tcKimlikNo &&
                              hc.ConnectionStatus == ConnectionStatus.online);
        }
    }
}