using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Iller
    {
        [Key]
        public int IlId { get; set; }
        public required string IlAdi { get; set; }
        public ICollection<Ilceler>? Ilceler_ { get; set; }
    }
}
