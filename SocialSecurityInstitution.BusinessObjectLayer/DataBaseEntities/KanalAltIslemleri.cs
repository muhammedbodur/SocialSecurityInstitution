using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
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
    public class KanalAltIslemleri
    {
        [Key]
        public int KanalAltIslemId { get; set; }

        //KanallarAlt tablosu ile ilişkili
        public int KanalAltId { get; set; }
        [ForeignKey("KanalAltId")]
        public required KanallarAlt KanallarAlt { get; set; }

        //HizmetBinaları tablosu ile ilişkili
        public int HizmetBinasiId { get; set; }
        [ForeignKey("HizmetBinasiId")]
        public required HizmetBinalari HizmetBinalari { get; set; }

        public int? KanalIslemId { get; set; }
        [ForeignKey("KanalIslemId")]
        public required KanalIslemleri KanalIslem { get; set; }

        public int? KioskIslemGrupId { get; set; }
        [ForeignKey("KioskIslemGrupId")]
        public KioskIslemGruplari KioskIslemGruplari { get; set; }

        public Aktiflik KanalAltIslemAktiflik { get; set; }


        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<Siralar> Siralar { get; set; }
    }
}
