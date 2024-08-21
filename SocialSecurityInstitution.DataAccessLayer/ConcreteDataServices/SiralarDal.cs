using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class SiralarDal : GenericRepository<Siralar, SiralarDto>, ISiralarDal
    {
        public SiralarDal(Context context, IMapper mapper, ILogService logService) : base(context, mapper, logService)
        { 
        }
    }
}
