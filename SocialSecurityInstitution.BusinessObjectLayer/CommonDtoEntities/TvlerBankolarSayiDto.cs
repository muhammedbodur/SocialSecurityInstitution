using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class TvlerBankolarSayiDto
    {
        public int TvId { get; set; }
        public KatTipi KatTipi { get; set; }
        public string KatTipiAdi { get; set; }
        public int BankoEslesmeSayisi { get; set; }
    }
}
