using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService
{
    public class HubConnectionCustomService : IHubConnectionCustomService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        private readonly ILogger<HubConnectionCustomService> _logger;

        public HubConnectionCustomService(IMapper mapper, Context context, ILogger<HubConnectionCustomService> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<HubConnectionDto> GetHubConnectionWithConnectionIdAsync(string connectionId)
        {
            var entity = await _context.HubConnection
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ConnectionId == connectionId);

            if (entity == null)
            {
                _logger.LogWarning($"ConnectionId {connectionId} ile eşleşen kayıt bulunamadı.");
                return null;
            }

            var dto = _mapper.Map<HubConnectionDto>(entity);
            return dto;
        }

        public async Task<HubConnectionDto> GetHubConnectionWithTcKimlikNoAsync(string tcKimlikNo)
        {
            var entity = await _context.HubConnection
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TcKimlikNo == tcKimlikNo);

            if (entity == null)
            {
                _logger.LogWarning($"TcKimlikNo {tcKimlikNo} ile eşleşen kayıt bulunamadı.");
                return null;
            }

            var dto = _mapper.Map<HubConnectionDto>(entity);
            return dto;
        }
    }
}
