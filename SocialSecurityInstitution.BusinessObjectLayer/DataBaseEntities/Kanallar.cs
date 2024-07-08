using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class Kanallar
    {
        [Key]
        public int KanalId { get; set; }
        public required string KanalAdi { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
        
        public ICollection<KanallarAlt>? KanallarAlt_ { get; set; }
        public ICollection<KanalIslemleri>? KanalIslemleri_ { get; set; }
    }
}
