using AutoMapper;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class LoginLogoutLogDal : GenericRepository<LoginLogoutLog, LoginLogoutLogDto>, ILoginLogoutLogDal
    {
        public LoginLogoutLogDal(IMapper mapper) : base(mapper)
        {
        }
    }
}
