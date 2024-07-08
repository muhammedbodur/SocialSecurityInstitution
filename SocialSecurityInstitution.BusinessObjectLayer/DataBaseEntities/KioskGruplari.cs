using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class KioskGruplari
    {
        [Key]
        public int KioskGrupId { get; set; }
        public required string KioskGrupAdi { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<KioskIslemGruplari>? KioskIslemGruplari_ { get; set; }
    }
}
