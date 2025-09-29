using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities
{
    public class HubTvConnection
    {
        [Key]
        public int HubTvConnectionId { get; set; }
        public int TvId { get; set; }
        [ForeignKey("TvId")]
        public Tvler Tvler { get; set; }
        public string? ConnectionId { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }
        public DateTime IslemZamani { get; set; } = DateTime.Now;
    }
}
