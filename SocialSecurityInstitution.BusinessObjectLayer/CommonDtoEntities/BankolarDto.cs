using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class BankolarDto
    {
        public int BankoId { get; set; }
        [Required]
        public int HizmetBinasiId { get; set; }
        [ForeignKey("HizmetBinasiId")]
        public HizmetBinalariDto HizmetBinalari { get; set; }
        public int BankoNo { get; set; }
        public Aktiflik BankoAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }

        public ICollection<BankolarKullaniciDto>? BankolarKullanici { get; set; }
    }
}
