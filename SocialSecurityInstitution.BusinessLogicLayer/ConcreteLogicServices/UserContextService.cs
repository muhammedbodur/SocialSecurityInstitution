using Microsoft.AspNetCore.Http;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices
{
    public class UserContextService : IUserContextService
    {
        public string TcKimlikNo { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string SessionID { get; set; }
        public int HizmetBinasiId { get; set; }
    }
}
