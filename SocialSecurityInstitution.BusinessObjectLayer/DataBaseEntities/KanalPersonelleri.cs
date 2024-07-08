using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class KanalPersonelleri
    {
        public readonly object Enums;

        [Key]
        public int KanalPersonelId { get; set; }

        public string TcKimlikNo { get; set; }
        [ForeignKey("TcKimlikNo")]
        public Personeller Personel { get; set; }
        public int KanalAltIslemId { get; set; }
        [ForeignKey("KanalAltIslemId")]
        public required KanalAltIslemleri KanalAltIslem { get; set; }
        public PersonelUzmanlik Uzmanlik { get; set; }
        public Aktiflik KanalAltIslemPersonelAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
