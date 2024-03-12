using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class KanalAltIslemleri
    {
        [Key]
        public int Id { get; set; }
        public required KanalIslemleri KanalIslem { get; set; }
        public required string KanalAltIslemAdi { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
