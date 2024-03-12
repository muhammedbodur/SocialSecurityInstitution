using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class HizmetBinalari
    {
        [Key]
        public int Id { get; set; }
        public required string HizmetBinasi { get; set; }
        public ICollection<Personeller>? Personeller_ { get; set; }
    }
}
