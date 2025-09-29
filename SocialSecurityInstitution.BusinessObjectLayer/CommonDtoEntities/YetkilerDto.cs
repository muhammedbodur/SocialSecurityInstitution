using System;
using System.Collections.Generic;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class YetkilerDto
    {
        public int YetkiId { get; set; }

        // Yetki türü (AnaYetki, OrtaYetki, AltYetki)
        public required YetkiTurleri YetkiTuru { get; set; }

        // Yetki adı ve açıklama
        public string YetkiAdi { get; set; } = null!;
        public string Aciklama { get; set; } = null!;

        // Eğer AnaYetki değilse, UstYetkiId gereklidir.
        public int UstYetkiId { get; set; }

        // Controller ve Action bilgileri
        public string? ControllerAdi { get; set; }
        public string? ActionAdi { get; set; }

        // Hiyerarşik yapıyı taşımak için Orta ve Alt Yetkiler
        public ICollection<YetkilerDto>? OrtaYetkiler { get; set; } = new List<YetkilerDto>(); // Orta Yetkiler
        public ICollection<YetkilerDto>? AltYetkiler { get; set; } = new List<YetkilerDto>();  // Alt Yetkiler

        // Ekleme ve düzenleme tarihleri
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
