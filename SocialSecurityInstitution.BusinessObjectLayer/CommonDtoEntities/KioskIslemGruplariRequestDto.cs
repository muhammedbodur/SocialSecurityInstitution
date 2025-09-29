using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.ValidationAttributes;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KioskIslemGruplariRequestDto
    {
        [PositiveNumber(AllowZero = true)]
        public int KioskIslemGrupId { get; set; }

        [Required(ErrorMessage = "Kiosk İşlem Grup Adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Kiosk İşlem Grup Adı 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public required string KioskIslemGrupAdi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Kiosk Grup seçiniz")]
        public int KioskGrupId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Hizmet Binası seçiniz")]
        public int HizmetBinasiId { get; set; }

        [Required(ErrorMessage = "Hizmet Binası Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Hizmet Binası Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public required string HizmetBinasiAdi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Departman seçiniz")]
        public int DepartmanId { get; set; }

        [Required(ErrorMessage = "Departman Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Departman Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public required string DepartmanAdi { get; set; }

        [Range(1, 999, ErrorMessage = "Kiosk İşlem Grup Sıra 1-999 arasında olmalıdır")]
        public int KioskIslemGrupSira { get; set; }

        [Required(ErrorMessage = "Kiosk İşlem Grup Aktiflik durumu seçimi zorunludur")]
        public Aktiflik KioskIslemGrupAktiflik { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EklenmeTarihi { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
