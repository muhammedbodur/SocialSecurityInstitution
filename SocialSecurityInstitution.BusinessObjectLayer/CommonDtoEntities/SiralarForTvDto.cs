using System;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class SiralarForTvDto
    {
        public int SiraId { get; set; }
        public int SiraNo { get; set; }
        public DateTime SiraAlisZamani { get; set; }
        public DateTime? IslemBaslamaZamani { get; set; }
        public BeklemeDurum BeklemeDurum { get; set; }
        public string TcKimlikNo { get; set; }
        public int BankoId { get; set; }
        public int BankoNo { get; set; }
        public BankoTipi BankoTipi { get; set; }
        public KatTipi KatTipi { get; set; }
        public string AdSoyad { get; set; }
        public int HizmetBinasiId { get; set; }
        public int TvId { get; set; }
        public string ConnectionId { get; set; }
    }
}
