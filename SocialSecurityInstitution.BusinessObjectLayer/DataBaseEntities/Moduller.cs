using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class Moduller
    {
        [Key]
        public int ModulId { get; set; }
        public required string ModulAdi { get; set; }
        public ICollection<ModullerAlt> ModullerAlt { get; set; } // Alt modüller
        public ICollection<ModulController> Controller { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
