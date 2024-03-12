using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class BankoIslemleriDal : GenericRepository<BankoIslemleri>, IBankoIslemleriDal
    {
        public BankoIslemleriDal()
        {
        }
    }
}
