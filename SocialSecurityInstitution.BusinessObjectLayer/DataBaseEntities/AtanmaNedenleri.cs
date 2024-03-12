using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class AtanmaNedenleri
    {
        /*
        Atanma Nedenleri artabileceğinden dolayı bunu tablo olarak tutmakta fayda var, şuan için 19 tane var 
        */
        [Key]
        public int Id { get; set; }
        public required string AtanmaNedeni { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
        public ICollection<Personeller>? Personeller_ { get; set; }
    }
}
