using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class ModulControllerIslemler
    {
        [Key]
        public int ControllerIslemId { get; set; }
        [Required]
        public required string ControllerIslemAdi { get; set; }
        public required ModulController Controller { get; set; }
        public ICollection<PersonelYetkileri> Yetki { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
