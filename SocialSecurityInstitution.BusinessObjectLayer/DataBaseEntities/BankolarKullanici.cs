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
        public int BankoKullaniciId { get; set; }

        public int BankoId { get; set; }
        [ForeignKey("BankoId")]
        public Bankolar Bankolar { get; set; }

        public string TcKimlikNo { get; set; }
        [ForeignKey("TcKimlikNo")]
        public Personeller Personel { get; set; }

        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}
