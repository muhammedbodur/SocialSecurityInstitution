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
    public class TvBankolarRequestDto
    {
        [PositiveNumber(AllowZero = true)]
        public int TvBankoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir TV seçiniz")]
        public int TvId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Banko seçiniz")]
        public int BankoId { get; set; }

        [Range(1, 9999, ErrorMessage = "Banko No 1-9999 arasında olmalıdır")]
        public int BankoNo { get; set; }

        [Required(ErrorMessage = "Banko Tipi seçimi zorunludur")]
        public BankoTipi BankoTipi { get; set; }

        [Required(ErrorMessage = "Kat Tipi seçimi zorunludur")]
        public KatTipi KatTipi { get; set; }

        [Required(ErrorMessage = "Banko Aktiflik durumu seçimi zorunludur")]
        public Aktiflik BankoAktiflik { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Hizmet Binası seçiniz")]
        public int HizmetBinasiId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EklenmeTarihi { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
