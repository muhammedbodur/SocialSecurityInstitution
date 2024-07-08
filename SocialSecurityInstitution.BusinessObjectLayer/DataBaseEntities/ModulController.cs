using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class ModulController
    {
        [Key]
        public int ControllerId { get; set; }
        [Required]
        public required string ControllerAdi { get; set; }
        public required Moduller Modul { get; set; }
        public ICollection<ModulControllerIslemler> ControllerIslem { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
