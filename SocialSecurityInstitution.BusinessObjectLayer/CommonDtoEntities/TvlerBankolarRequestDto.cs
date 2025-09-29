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
    public class TvlerBankolarRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir TV seçiniz")]
        public int TvId { get; set; }

        [Required(ErrorMessage = "Kat Tipi seçimi zorunludur")]
        public KatTipi KatTipi { get; set; }

        [Required(ErrorMessage = "Kat Tipi Adı zorunludur")]
        [StringLength(50, ErrorMessage = "Kat Tipi Adı 50 karakterden fazla olamaz")]
        [TurkishText]
        public required string KatTipiAdi { get; set; }

        [Range(0, 999, ErrorMessage = "Banko Eşleşme Sayısı 0-999 arasında olmalıdır")]
        public int BankoEslesmeSayisi { get; set; }
    }
}
