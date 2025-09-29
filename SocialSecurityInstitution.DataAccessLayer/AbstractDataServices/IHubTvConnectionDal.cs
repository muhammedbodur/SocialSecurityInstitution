using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.AbstractDataServices
{
    public interface IHubTvConnectionDal : IGenericDal<HubTvConnectionDto>
    {
        Task<HubTvConnectionDto> GetHubTvConnectionByConnectionIdAsync(string connectionId);
        Task<HubTvConnectionDto> GetHubTvConnectionByTvIdAsync(int tvId);
    }
}
