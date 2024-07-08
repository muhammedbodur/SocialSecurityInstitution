using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class ModulControllerDto
    {
        public int Id { get; set; }
        public required string ControllerAdi { get; set; }
        public required Moduller Modul { get; set; }
        public ICollection<ModulControllerIslemler> ControllerIslem { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
