using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class SiralarWithConnectionIdDto
    {
        public int SiraId { get; set; }
        public int SiraNo { get; set; }
        public string? TcKimlikNo { get; set; } // TcKimlikNo Boş olabilir, sıra yı kimse çağırmamıştır.  
        public int BankoNo { get; set; }
        public string? ConnectionId { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
        public int KanalAltIslemId { get; set; }
        public DateTime SiraAlisZamani { get; set; }
        public DateTime? IslemBaslamaZamani { get; set; }
        public DateTime? IslemBitisZamani { get; set; }
        public BeklemeDurum BeklemeDurum { get; set; }
        public DateTime SiraAlisTarihi { get; set; }
        public int KanalAltId { get; set; }
        public string? KanalAltAdi { get; set; }
        public int HizmetBinasiId { get; set; }
        public string? AdSoyad { get; set; }
        public PersonelUzmanlik Uzmanlik { get; set; }
    }
}
