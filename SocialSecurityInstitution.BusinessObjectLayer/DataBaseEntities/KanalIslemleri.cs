using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class KanalIslemleri
    {
        [Key]
        public int Id { get; set; }
        public required string KanalIslemAdi { get; set; }
        public int BaslangicNumara { get; set; }
        public int BitisNumara { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<KanalAltIslemleri>? KanalAltIslemleri_ { get; set; }


        /*Farkın 500 olması koşulunu kontrol eden özel bir metot, burada geliştirme olacak 1-9999 aralığında
         olacak ve 20 tane Kanaldan fazla olursa 500 den daha düşük bir aralık seçilmesi lazım, ona göre fonksiyonu 
        optimize etmemiz lazım
         */
        public bool FarkKosulunuKontrolEt()
        {
            return Math.Abs(BitisNumara - BaslangicNumara) == 500;
        }
    }
}
