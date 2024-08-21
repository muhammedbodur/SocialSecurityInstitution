using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class SiralarTvListeDto
    {
        public int SiraId { get; set; }
        public int SiraNo { get; set; }
        public int HizmetBinasiId { get; set; }
        public DateTime SiraAlisZamani { get; set; }
        public DateTime? IslemBaslamaZamani { get; set; }
        public BeklemeDurum BeklemeDurum { get; set; }
        public required string TcKimlikNo { get; set; }
        public required string AdSoyad { get; set; }
        public int BankoId { get; set; }
        public int BankoNo { get;set; }
        public BankoTipi BankoTipi { get; set; }
        public KatTipi KatTipi { get; set; }
    }
}
