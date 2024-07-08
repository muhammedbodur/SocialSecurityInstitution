using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class ModulDto
    {
        public int Id { get; set; }
        public required string ModulAdi { get; set; }
        public ICollection<ModulController> Controller { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
