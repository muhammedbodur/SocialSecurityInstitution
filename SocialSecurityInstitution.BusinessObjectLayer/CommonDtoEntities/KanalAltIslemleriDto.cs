using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalAltIslemleriDto
    {
        public int Id { get; set; }
        public required KanalIslemleri KanalIslem { get; set; }
        public required string KanalAltIslemAdi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
