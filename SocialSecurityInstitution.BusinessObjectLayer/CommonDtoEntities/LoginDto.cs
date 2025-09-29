using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.ValidationAttributes;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class LoginDto
    {
        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [TcKimlikNoValidation]
        public required string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public required string AdSoyad { get; set; }

        [Required(ErrorMessage = "Email zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(100, ErrorMessage = "Email 100 karakterden fazla olamaz")]
        public required string Email { get; set; }

        [StringLength(500, ErrorMessage = "Resim yolu 500 karakterden fazla olamaz")]
        public required string Resim { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Hizmet Binası seçiniz")]
        public int HizmetBinasiId { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre 6-100 karakter arasında olmalıdır")]
        [NoHtmlContent]
        public string? PassWord { get; set; }
    }
}
