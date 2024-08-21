using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class siraCagirmaDto
    {
        public int SiraId { get; set; }
        public int SiraNo { get; set; }
        public int BankoNo { get; set; }
        public DateTime? SiraAlisZamani { get; set; }
        public DateTime? IslemBaslamaZamani { get; set; }
        public DateTime? IslemBitisZamani { get; set; }
        public BeklemeDurum BeklemeDurum { get; set; }
        public int KanalAltIslemId { get; set; }
        public int KanalAltId { get; set; }
        public string KanalAltAdi { get; set; } = string.Empty;
        public string TcKimlikNo { get; set; } = string.Empty;
        public string AdSoyad { get; set; } = string.Empty;
        public PersonelUzmanlik PersonelUzmanlik { get; set; }
        public string IslemiYapan { get; set; } = string.Empty;
    }
}
