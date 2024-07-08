using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KioskIslemGruplariAltIslemlerEslestirmeSayisiRequestDto
    {
        public int KioskIslemGrupId { get; set; }
        public required string KioskIslemGrupAdi { get; set; }
        public int? KioskIslemGrupSira { get; set; }
        public int EslestirmeSayisi { get; set; }
    }
}
