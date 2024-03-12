using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Sendikalar
    {
        [Key] 
        public int Id { get; set; }
        public required string SendikaAdi { get; set; }
        public ICollection<Personeller>? Personeller_ { get; set; }
    }
}
