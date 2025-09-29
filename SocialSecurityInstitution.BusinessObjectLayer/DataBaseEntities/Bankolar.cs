using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Bankolar
    {
        [Key]
        public int BankoId { get; set; }
        [Required]
        public int HizmetBinasiId { get; set; }
        [ForeignKey("HizmetBinasiId")]
        public HizmetBinalari HizmetBinalari { get; set; }
        public int BankoNo { get; set; }
        public BankoTipi BankoTipi { get; set; }
        public KatTipi KatTipi { get; set; }
        public Aktiflik BankoAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<BankolarKullanici>? BankolarKullanici { get; set; }
        public ICollection<TvBankolari>? TvBankolari { get; set; }
    }
}
