using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices
{
    public class PdksCihazlarDal : GenericRepository<PdksCihazlar>, IPdksCihazlarDal
    {
        public PdksCihazlarDal()
        {
        }
    }
}
