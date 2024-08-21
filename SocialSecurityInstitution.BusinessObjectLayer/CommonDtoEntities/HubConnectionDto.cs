using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class HubConnectionDto
    {
        public int HubConnectionId { get; set; }
        public string? TcKimlikNo { get; set; }
        public string? ConnectionId { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
        public DateTime IslemZamani { get; set; }
    }
}
