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
    public class KanalPersonelleriDal : GenericRepository<KanalPersonelleri, KanalPersonelleriDto>, IKanalPersonelleriDal
    {
        public KanalPersonelleriDal(IMapper mapper) : base(mapper)
        {
        }
    }
}
