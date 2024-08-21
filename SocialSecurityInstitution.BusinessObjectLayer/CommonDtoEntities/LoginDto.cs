using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class LoginDto
    {
        public required string TcKimlikNo { get; set; }
        public required string AdSoyad { get; set; }
        public required string Email { get; set; }
        public required string Resim { get; set; }
        public int HizmetBinasiId { get; set; }
        public string? PassWord { get;}
    }
}
