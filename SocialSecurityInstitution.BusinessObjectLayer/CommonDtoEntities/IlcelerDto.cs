using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class IlcelerDto
    {
        public int IlceId { get; set; }
        public int IlId { get; set; }
        public required IllerDto Iller { get; set; }
        public required string IlceAdi { get; set; }
    }
}
