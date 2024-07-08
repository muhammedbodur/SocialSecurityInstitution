using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KioskIslemGruplariDto
    {
        public int KioskIslemGrupId { get; set; }
        public required string KioskIslemGrupAdi { get; set; }
        public int KioskGrupId { get; set; }
        public int HizmetBinasiId { get; set; }

        public int KioskIslemGrupSira { get; set; }
        public Aktiflik KioskIslemGrupAktiflik { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }

        public ICollection<KanalAltIslemleriDto>? KanalAltIslemleriDto_ { get; set; }
    }
}
