using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class SiraNoBilgisiDto
    {
        public int SiraNo { get; set; }
        public int HizmetBinasiId { get; set; }
        public required string HizmetBinasiAdi { get; set; }
        public required string KanalAltAdi { get; set; }
    }
}
