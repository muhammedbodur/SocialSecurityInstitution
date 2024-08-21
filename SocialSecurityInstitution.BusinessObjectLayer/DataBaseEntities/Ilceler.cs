using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Ilceler
    {
        [Key]
        public int IlceId { get; set; }
        public int IlId { get; set; }
        [ForeignKey("IlId")]
        public required Iller Il { get; set; }
        public required string IlceAdi { get; set; }
    }
}
