using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalAltIslemleriEslestirmeSayisiRequestDto
    {
        public int KanalIslemId { get; set; }
        public required string KanalIslemAdi { get; set; }
        public int EslestirmeSayisi { get; set; }
    }
}
