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
    public class KanalAltIslemleriRequestDto
    {
        [PositiveNumber(AllowZero = true)]
        public int KanalAltIslemId { get; set; }

        [Required(ErrorMessage = "Kanal Alt İşlem Adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Kanal Alt İşlem Adı 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public required string KanalAltIslemAdi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Kanal Alt seçiniz")]
        public int KanalAltId { get; set; }

        [Required(ErrorMessage = "Kanal Alt Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Kanal Alt Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public required string KanalAltAdi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Hizmet Binası seçiniz")]
        public int HizmetBinasiId { get; set; }

        [Required(ErrorMessage = "Hizmet Binası Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Hizmet Binası Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public required string HizmetBinasiAdi { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int? KanalIslemId { get; set; }

        [StringLength(100, ErrorMessage = "Kanal İşlem Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? KanalIslemAdi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Departman seçiniz")]
        public int DepartmanId { get; set; }

        [Required(ErrorMessage = "Departman Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Departman Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public required string DepartmanAdi { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int? KioskIslemGrupId { get; set; }

        [StringLength(100, ErrorMessage = "Kiosk İşlem Grup Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? KioskIslemGrupAdi { get; set; }

        [Required(ErrorMessage = "Kanal Alt İşlem Aktiflik durumu seçimi zorunludur")]
        public Aktiflik KanalAltIslemAktiflik { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EklenmeTarihi { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
