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
    public class KanalIslemleri
    {
        [Key]
        public int KanalIslemId { get; set; }
        public int KanalId { get; set; }
        [ForeignKey("KanalId")]
        public Kanallar Kanallar { get; set; }

        public int HizmetBinasiId { get; set; }
        [ForeignKey("HizmetBinasiId")]
        public HizmetBinalari HizmetBinalari { get; set; }

        [Range(0, 9999, ErrorMessage = "BaslangicNumara 0 ile 9999 arasında olmalıdır.")]
        public int BaslangicNumara { get; set; }

        [Range(0, 9999, ErrorMessage = "BitisNumara 0 ile 9999 arasında olmalıdır.")]
        public int BitisNumara { get; set; }

        public Aktiflik KanalIslemAktiflik { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<KanalAltIslemleri>? KanalAltIslemleri_ { get; set; }
    }
}
