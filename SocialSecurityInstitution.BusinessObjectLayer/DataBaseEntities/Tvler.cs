using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class Tvler
    {
        [Key]
        public int TvId { get; set; }
        public int HizmetBinasiId { get; set; }
        [ForeignKey("HizmetBinasiId")]
        public HizmetBinalari HizmetBinalari { get; set; }
        public KatTipi KatTipi { get; set; }
        public string? Aciklama { get; set; }
        public DateTime IslemZamani { get; set; } = DateTime.Now;

        public HubTvConnection? HubTvConnection { get; set; }
        public ICollection<TvBankolari>? TvBankolari { get; set; }
    }
}
