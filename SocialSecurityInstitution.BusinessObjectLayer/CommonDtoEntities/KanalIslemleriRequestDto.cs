using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalIslemleriRequestDto
    {
        public int KanalIslemId { get; set; }
        public required string KanalIslemAdi { get; set; }

        public int HizmetBinasiId { get; set; }
        public required string HizmetBinasiAdi { get; set; }

        public int DepartmanId { get; set; }
        public required string DepartmanAdi { get; set; }

        public int BaslangicNumara { get; set; }

        public int BitisNumara { get; set; }

        public Aktiflik KanalIslemAktiflik { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
