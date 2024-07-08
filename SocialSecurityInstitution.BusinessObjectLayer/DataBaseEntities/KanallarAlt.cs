using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class KanallarAlt
    {
        [Key]
        public int KanalAltId { get; set; }
        public required string KanalAltAdi { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<KanalAltIslemleri>? KanalAltIslemleri_ { get; set; }
    }
}
