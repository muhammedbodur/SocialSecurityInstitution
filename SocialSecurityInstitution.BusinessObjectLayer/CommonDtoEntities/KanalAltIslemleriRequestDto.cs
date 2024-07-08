using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalAltIslemleriRequestDto
    {
        public int KanalAltIslemId { get; set; }
        public required string KanalAltIslemAdi { get; set; }

        public int KanalAltId { get; set; }
        public required string KanalAltAdi { get; set; }

        public int HizmetBinasiId { get; set; }
        public required string HizmetBinasiAdi { get; set; }

        public int? KanalIslemId { get; set; }
        public string? KanalIslemAdi { get; set; }

        public int DepartmanId { get; set; }
        public required string DepartmanAdi { get; set; }

        public int? KioskIslemGrupId { get; set; }
        public string? KioskIslemGrupAdi { get; set; }

        public Aktiflik KanalAltIslemAktiflik { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
