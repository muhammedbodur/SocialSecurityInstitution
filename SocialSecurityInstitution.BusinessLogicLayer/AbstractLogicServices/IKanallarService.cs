using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;

namespace SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices
{
    public interface IKanallarService : IGenericService<Kanallar, KanallarDto>
    {
    }
}
