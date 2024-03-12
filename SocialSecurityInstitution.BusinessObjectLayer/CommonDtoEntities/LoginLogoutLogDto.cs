using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class LoginLogoutLogDto
    {
        public int Id { get; set; }
        public required string TcKimlikNo { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
    }
}
