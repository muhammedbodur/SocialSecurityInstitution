using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Yetkiler
    {
        [Key]
        public int YetkiId { get; set; }

        // Yetki türü (Ana, Orta, Alt Yetki)
        public required YetkiTurleri YetkiTuru { get; set; }

        // Yetki adı ve açıklama
        public string YetkiAdi { get; set; } = null!;
        public string Aciklama { get; set; } = null!;

        /*
         UstYetkiId, yetkinin üst yetkisini temsil eder.
         Eğer AnaYetki ise UstYetkiId -1 olabilir.
        */
        public int? UstYetkiId { get; set; }

        // Controller ve Action ile ilişkilendirme
        public string? ControllerAdi { get; set; }  // Ana Yetki için Controller atanır
        public string? ActionAdi { get; set; }      // Orta ve Alt Yetkiler için Action atanır

        // Ekleme ve düzenleme tarihleri
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        // Personel ile Yetki ilişkisi
        public ICollection<PersonelYetkileri>? PersonelYetkileri { get; set; }
    }
}
