using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalAltIslemleriDto
    {
        public int KanalAltIslemId { get; set; }
        public string? KanalAltIslemAdi { get; set; }
        //KanallarAlt tablosu ile ilişkili
        public int KanalAltId { get; set; }
        //HizmetBinaları tablosu ile ilişkili
        public int HizmetBinasiId { get; set; }
        public int? KanalIslemId { get; set; }
        
        public int? KioskIslemGrupId { get; set; }
        public Aktiflik KanalAltIslemAktiflik { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
