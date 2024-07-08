using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalIslemleriDto
    {
        public int KanalIslemId { get; set; }
        public int KanalId { get; set; }

        public string KanalAdi { get; set; }

        public int HizmetBinasiId { get; set; }

        [Range(0, 9999, ErrorMessage = "BaslangicNumara 0 ile 9999 arasında olmalıdır.")]
        public int BaslangicNumara { get; set; }

        [Range(0, 9999, ErrorMessage = "BitisNumara 0 ile 9999 arasında olmalıdır.")]
        public int BitisNumara { get; set; }

        public Aktiflik KanalIslemAktiflik { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }

        public ICollection<KanalAltIslemleriDto>? KanalAltIslemleriDto_ { get; set; }
    }
}
