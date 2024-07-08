using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class KioskIslemGruplari
    {
        [Key]
        public int KioskIslemGrupId { get; set; }

        public int KioskGrupId { get; set; }
        [ForeignKey("KioskGrupId")]
        public KioskGruplari KioskGruplari { get; set; }

        public int HizmetBinasiId { get; set; }
        [ForeignKey("HizmetBinasiId")]
        public HizmetBinalari HizmetBinalari { get; set; }

        public int KioskIslemGrupSira { get; set; }
        public Aktiflik KioskIslemGrupAktiflik { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<KanallarAlt>? KanallarAlt_ { get; set; }
    }
}
