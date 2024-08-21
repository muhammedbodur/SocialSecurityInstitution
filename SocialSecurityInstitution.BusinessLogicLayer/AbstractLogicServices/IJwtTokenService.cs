using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(string tcKimlikNo, string adSoyad, string email, string sessionId, int hizmetBinasiId);
    }
}
