using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.ValidationAttributes;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalAltIslemleriEslestirmeSayisiRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Kanal İşlem seçiniz")]
        public int KanalIslemId { get; set; }

        [Required(ErrorMessage = "Kanal İşlem Adı zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Kanal İşlem Adı 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public required string KanalIslemAdi { get; set; }

        [Range(0, 9999, ErrorMessage = "Eşleştirme Sayısı 0-9999 arasında olmalıdır")]
        public int EslestirmeSayisi { get; set; }
    }
}
