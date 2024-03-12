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
        [Key]
        public int Id { get; set; }
        public required string TcKimlikNo { get; set; }
        public required KanalAltIslemleri KanalAltIslem { get; set; }
        public Aktiflik PersonelKanalAltIslemAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        [ForeignKey("TcKimlikNo")]
        public required Personeller Personel { get; set; }
    }
}
