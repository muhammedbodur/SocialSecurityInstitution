using AutoMapper;
using SocialSecurityInstitution.BusinessObjectLayer;
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
    public class AtanmaNedenleriDal : GenericRepository<AtanmaNedenleri, AtanmaNedenleriDto>, IAtanmaNedenleriDal
    {
        public AtanmaNedenleriDal(IMapper mapper) : base(mapper)
        {
        }
    }
}

