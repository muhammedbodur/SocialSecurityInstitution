using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalPersonelleriDto
    {
        public int Id { get; set; }
        public required string TcKimlikNo { get; set; }
        public required KanalAltIslemleri KanalAltIslem { get; set; }
        public Aktiflik PersonelKanalAltIslemAktiflik { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
