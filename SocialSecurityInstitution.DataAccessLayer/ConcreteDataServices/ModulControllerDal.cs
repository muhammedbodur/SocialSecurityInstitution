using AutoMapper;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class ModulControllerDal : GenericRepository<ModulController, ModulControllerDto>, IModulControllerDal
    {
        public ModulControllerDal(IMapper mapper) : base(mapper)
        {
        }
    }
}
