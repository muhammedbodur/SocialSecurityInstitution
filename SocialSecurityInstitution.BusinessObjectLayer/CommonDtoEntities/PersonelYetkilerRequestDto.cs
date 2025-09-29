using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.ValidationAttributes;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonelYetkilerRequestDto
    {
        [PositiveNumber(AllowZero = true)]
        public int YetkiId { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [TcKimlikNoValidation]
        public string TcKimlikNo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Yetki Tipi seçiniz")]
        public int YetkiTipi { get; set; }
    }
}
