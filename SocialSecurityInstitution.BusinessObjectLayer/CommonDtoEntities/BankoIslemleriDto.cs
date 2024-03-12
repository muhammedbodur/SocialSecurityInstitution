using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class BankoIslemleriDto
    {
        public int Id { get; set; }
        public required BankoGrup BankoGrup { get; set; }
        public int BankoUstIslemId { get; set; }
        public required string BankoIslemAdı { get; set; }
        public int BankoIslemSira { get; set; }
        public Aktiflik BankoIslemAktiflik { get; set; }
        public string? DiffLang { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
