using AutoMapper;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class SiralarDal : GenericRepository<SiralarDal, SiralarDto>, ISiralarDal
    {
        public SiralarDal(IMapper mapper) : base(mapper)
        {
        }
    }
}
