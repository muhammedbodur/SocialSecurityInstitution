using System;
using System.Collections.Generic;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class YetkilerWithPersonelDto
    {
        public int YetkiId { get; set; }
        public YetkiTurleri YetkiTuru { get; set; }
        public string YetkiAdi { get; set; } = null!;
        public string Aciklama { get; set; } = null!;
        public int? UstYetkiId { get; set; }
        public string? ControllerAdi { get; set; }
        public string? ActionAdi { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public List<PersonelYetkileriDto>? PersonelYetkileri { get; set; } = new List<PersonelYetkileriDto>();
        public List<YetkilerWithPersonelDto>? OrtaYetkiler { get; set; } = new List<YetkilerWithPersonelDto>();
        public List<YetkilerWithPersonelDto>? AltYetkiler { get; set; } = new List<YetkilerWithPersonelDto>();
    }
}
