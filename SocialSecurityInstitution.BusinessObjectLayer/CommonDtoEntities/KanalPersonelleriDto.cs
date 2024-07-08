using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalPersonelleriDto
    {
        public int KanalPersonelId { get; set; }
        public string TcKimlikNo { get; set; }
        public int KanalAltIslemId { get; set; }
        public PersonelUzmanlik Uzmanlik { get; set; }
        public Aktiflik KanalAltIslemPersonelAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
