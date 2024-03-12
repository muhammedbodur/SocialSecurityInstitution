using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class LoginLogoutLog
    {
        [Key]
        public int Id { get; set; }
        public required string TcKimlikNo { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }

        [ForeignKey("TcKimlikNo")]
        public required Personeller Personel { get; set; }
    }
}
