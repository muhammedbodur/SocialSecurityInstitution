using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class BankolarKullanici
    {
        [Key]
        public int Id { get; set; }

        public int BankoId { get; set; }
        [ForeignKey("BankoId")]
        public Bankolar? Banko { get; set; }
        public required string TcKimlikNo { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }

}
