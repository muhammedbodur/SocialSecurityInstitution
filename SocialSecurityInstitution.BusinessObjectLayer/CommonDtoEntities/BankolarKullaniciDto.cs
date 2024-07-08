using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class BankolarKullaniciDto
    {
        public int BankoKullaniciId { get; set; }

        public int BankoId { get; set; }
        [ForeignKey("BankoId")]
        public BankolarDto Bankolar { get; set; }
        public string TcKimlikNo { get; set; }
        [ForeignKey("TcKimlikNo")]
        public PersonellerDto Personel { get; set; }

        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
