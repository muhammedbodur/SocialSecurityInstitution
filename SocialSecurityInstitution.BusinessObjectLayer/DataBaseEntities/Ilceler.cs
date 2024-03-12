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
        public int Id { get; set; }
        public required Iller Il { get; set; }
        public required string IlceAdi { get; set; }
    }
}
