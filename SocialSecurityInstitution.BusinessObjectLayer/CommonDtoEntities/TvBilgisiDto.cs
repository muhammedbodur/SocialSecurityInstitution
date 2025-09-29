using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class TvBilgisiDto
    {
        public int TvId { get; set; }
        public int HizmetBinasiId { get; set; }
        public string HizmetBinasiAdi { get; set; }
        public string Aciklama { get; set; }
    }
}
