using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class TvBankolari
    {
        [Key]
        public int TvBankoId { get; set; }
        public int TvId { get; set; }
        [ForeignKey("TvId")]
        public required Tvler Tvler { get; set; }
        public int BankoId { get; set; }
        [ForeignKey("BankoId")]
        public required Bankolar Bankolar { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
