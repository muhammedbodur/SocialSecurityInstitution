using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class HubTvConnectionDal : GenericRepository<HubTvConnection, HubTvConnectionDto>, IHubTvConnectionDal
    {
        public HubTvConnectionDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }

        public async Task<HubTvConnectionDto> GetHubTvConnectionByConnectionIdAsync(string connectionId)
        {
            var entity = await _context.HubTvConnection
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ConnectionId == connectionId);

            return _mapper.Map<HubTvConnectionDto>(entity);
        }

        public async Task<HubTvConnectionDto> GetHubTvConnectionByTvIdAsync(int tvId)
        {
            var entity = await _context.HubTvConnection
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TvId == tvId);

            return _mapper.Map<HubTvConnectionDto>(entity);
        }
    }
}
