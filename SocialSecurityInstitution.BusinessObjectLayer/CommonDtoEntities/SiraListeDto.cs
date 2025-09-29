using System;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class SiraListeDto
    {
        public int SiraId { get; set; }
        public int SiraNo { get; set; }
        public int KanalAltIslemId { get; set; }
        public string KanalAltAdi { get; set; }
        public int HizmetBinasiId { get; set; }
        public string HizmetBinasiAdi { get; set; }
        public string TcKimlikNo { get; set; }
        public string AdSoyad { get; set; }
        public DateTime SiraAlisZamani { get; set; }
        public DateTime? IslemBaslamaZamani { get; set; }
        public BeklemeDurum BeklemeDurum { get; set; }
        public string BeklemeDurumAdi { get; set; }
        public int BankoId { get; set; }
        public int BankoNo { get; set; }
        public BankoTipi BankoTipi { get; set; }
        public string BankoTipiAdi { get; set; }
    }
}
