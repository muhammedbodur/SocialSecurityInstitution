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
    public class PersonelAltKanallariRequestDto
    {
        [PositiveNumber(AllowZero = true)]
        public int KanalPersonelId { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [TcKimlikNoValidation]
        public string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public string AdSoyad { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Kanal Alt İşlem seçiniz")]
        public int KanalAltIslemId { get; set; }

        [Required(ErrorMessage = "Kanal Alt İşlem Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Kanal Alt İşlem Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string KanalAltIslemAdi { get; set; }

        [Required(ErrorMessage = "Uzmanlık seçimi zorunludur")]
        public PersonelUzmanlik Uzmanlik { get; set; }
    }
}
