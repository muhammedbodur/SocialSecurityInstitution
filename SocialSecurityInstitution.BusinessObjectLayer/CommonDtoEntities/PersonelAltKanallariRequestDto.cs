using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonelAltKanallariRequestDto
    {
        public int KanalPersonelId { get; set; }
        public string TcKimlikNo { get; set; }
        public string AdSoyad { get; set; }
        public int KanalAltIslemId { get; set; }
        public string KanalAltIslemAdi { get; set; }
        public PersonelUzmanlik Uzmanlik { get; set; }
    }
}
