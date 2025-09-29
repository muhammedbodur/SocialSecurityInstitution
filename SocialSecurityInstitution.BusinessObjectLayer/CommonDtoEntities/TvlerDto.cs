using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class TvlerDto
    {
        public int TvId { get; set; }
        public int HizmetBinasiId { get; set; }
        public KatTipi KatTipi { get; set; }
        public string? Aciklama { get; set; }
        public DateTime IslemZamani { get; set; }
    }
}
