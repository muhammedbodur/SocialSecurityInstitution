using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.ValidationAttributes;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Kiosk İşlem Grup seçiniz")]
        public int KioskIslemGrupId { get; set; }

        [Required(ErrorMessage = "Kiosk İşlem Grup Adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Kiosk İşlem Grup Adı 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public required string KioskIslemGrupAdi { get; set; }

        [Range(1, 999, ErrorMessage = "Kiosk İşlem Grup Sıra 1-999 arasında olmalıdır")]
        public int? KioskIslemGrupSira { get; set; }

        [Range(0, 9999, ErrorMessage = "Eşleştirme Sayısı 0-9999 arasında olmalıdır")]
        public int EslestirmeSayisi { get; set; }
    }
}
