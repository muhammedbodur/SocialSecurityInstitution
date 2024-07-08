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

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class KioskGruplariDal : GenericRepository<KioskGruplari, KioskGruplariDto>, IKioskGruplariDal
    {
        public KioskGruplariDal(IMapper mapper) : base(mapper)
        {
        }
    }
}
