using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class SiralarDto
    {
        public int SiraId { get; set; }
        public int Sira { get; set; }
        public int? BankoIslemId { get; set; }
        public int KanalAltIslemId { get; set; }
        public DateTime SiraAlisZamani { get; set; }
        public DateTime? IslemBaslamaZamani { get; set; }
        public DateTime? IslemBitisZamani { get; set; }
        public BeklemeDurum BeklemeDurum { get; set; }
    }
}
