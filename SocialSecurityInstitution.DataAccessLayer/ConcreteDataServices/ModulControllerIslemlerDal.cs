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
    internal class ModulControllerIslemlerDal : GenericRepository<ModulControllerIslemler, ModulControllerIslemlerDto>, IModulControllerIslemlerDal
    {
        public ModulControllerIslemlerDal(IMapper mapper) : base(mapper)
        {
        }
    }
}
