using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonellerLiteDto
    {
        public required string TcKimlikNo { get; set; }
        public int SicilNo { get; set; }
        public required string AdSoyad { get; set; }
        public int DepartmanId { get; set; }
        public int HizmetBinasiId { get; set; }
        public string? SessionID { get; set; }
        public string? ConnectionId { get; set; }
        public ConnectionStatus? ConnectionStatus { get; set; }
    }
}
