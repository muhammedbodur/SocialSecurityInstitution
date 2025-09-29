using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.ValidationAttributes;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonellerAltKanallarRequestDto
    {
        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [TcKimlikNoValidation]
        public string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public string AdSoyad { get; set; }

        [Range(0, 999, ErrorMessage = "Uzman Sayısı 0-999 arasında olmalıdır")]
        public int UzmanSayisi { get; set; }

        [Range(0, 999, ErrorMessage = "Uzman Yardımcısı Sayısı 0-999 arasında olmalıdır")]
        public int UzmanYrdSayisi { get; set; }
    }
}
