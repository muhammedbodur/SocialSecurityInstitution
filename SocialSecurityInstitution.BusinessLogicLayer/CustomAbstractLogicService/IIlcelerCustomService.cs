using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService
{
    public interface IIlcelerCustomService
    {
        Task<List<IlcelerDto>> GetIlcelerByIlIdAsync(int ilId);
        Task<List<IlcelerDto>> GetActiveIlcelerByIlIdAsync(int ilId);
    }
}
