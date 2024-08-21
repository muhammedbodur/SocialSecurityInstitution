using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class SiralarListeDto
    {
        public int SiraId { get; set; }
        public int SiraNo { get; set; }
        public DateTime SiraAlisZamani { get; set; }
        public int KanalAltIslemId { get; set; }
        public string? KanalAltAdi { get; set; }
        public int HizmetBinasiId { get; set; }
        public PersonelUzmanlik Uzmanlik { get; set; }
    }
}
