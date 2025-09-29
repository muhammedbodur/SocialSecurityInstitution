using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class PersonelYetkileri
    {
        [Key]
        public int PersonelYetkiId { get; set; }

        // Personel ile ilişki
        public string TcKimlikNo { get; set; }
        [ForeignKey("TcKimlikNo")]
        public required Personeller Personeller { get; set; }

        // Yetki ile ilişki
        public int YetkiId { get; set; }
        [ForeignKey("YetkiId")]
        public Yetkiler Yetki { get; set; }

        // Yetki Tipi (Gör, Düzenle vb.)
        public YetkiTipleri YetkiTipleri { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
