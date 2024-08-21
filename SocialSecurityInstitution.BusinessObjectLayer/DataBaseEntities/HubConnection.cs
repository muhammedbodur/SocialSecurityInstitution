using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class HubConnection
    {
        public int HubConnectionId { get; set; }
        public required string TcKimlikNo { get; set; }
        [ForeignKey("TcKimlikNo")]
        public Personeller? Personeller { get; set; }
        public string? ConnectionId { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
        public DateTime IslemZamani { get; set; } = DateTime.Now;
    }
}
