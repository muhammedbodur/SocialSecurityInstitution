using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IHubConnectionCustomService
    {
        Task<HubConnectionDto> GetHubConnectionWithTcKimlikNoAsync(string tcKimlikNo);
        Task<HubConnectionDto> GetHubConnectionWithConnectionIdAsync(string connectionId);
    }
}
