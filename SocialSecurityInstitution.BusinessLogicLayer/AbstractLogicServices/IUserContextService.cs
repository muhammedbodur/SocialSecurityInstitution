using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices
{
    public interface IUserContextService
    {
        string TcKimlikNo { get; set; }
        string AdSoyad { get; set; }
        string Email { get; set; }
        string SessionID { get; set; }
        int HizmetBinasiId { get; set; }
    }
}
