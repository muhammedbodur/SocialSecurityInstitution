using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class YetkilerDto
    {
        public int Id { get; set; }
        public required YetkiTurleri YetkiTuru { get; set; }
        public required string YetkiAdi { get; set; }
        public string? Aciklama { get; set; }
        public int UstYetkiId { get; set; }
        public Aktiflik YetkiAktiflik { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
