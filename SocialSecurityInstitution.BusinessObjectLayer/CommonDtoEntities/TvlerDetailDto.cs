using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class TvlerDetailDto
    {
        public int TvId { get; set; }
        public KatTipi KatTipi { get; set; }
        public string? Aciklama { get; set; }
        public int HizmetBinasiId { get; set; }
        public string? HizmetBinasiAdi { get; set; }
        public int DepartmanId { get; set; }
        public string? DepartmanAdi { get; set; }
    }
}
