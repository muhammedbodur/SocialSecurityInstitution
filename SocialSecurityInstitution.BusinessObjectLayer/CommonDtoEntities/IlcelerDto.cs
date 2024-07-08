using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class IlcelerDto
    {
        public int Id { get; set; }
        public required Iller Il { get; set; }
        public required string IlceAdi { get; set; }
    }
}
