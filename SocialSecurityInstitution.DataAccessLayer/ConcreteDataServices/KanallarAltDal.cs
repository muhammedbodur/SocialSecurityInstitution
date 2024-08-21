using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class KanallarAltDal : GenericRepository<KanallarAlt, KanallarAltDto>, IKanallarAltDal
    {
        public KanallarAltDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        {
        }
    }
}
