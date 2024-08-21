using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KullaniciBilgileriDto
    {
        public string TcKimlikNo { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Resim { get; set; }
        public string SessionID { get; set; }
        public int HizmetBinasiId { get; set; }
    }

}
