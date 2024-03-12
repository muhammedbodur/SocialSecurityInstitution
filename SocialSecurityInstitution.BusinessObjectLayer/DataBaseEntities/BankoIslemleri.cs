using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class BankoIslemleri
    {
        /*
         Bu tablo admin tarafından girilerek oluşturulacak, AnaGrup ise direkt devam edilecek, 
         OrtaGrup ise Anagrup a bağlanacak, AltGrup ise OrtaGrup a bağlanacak ve bu şekilde bir yapı oluşturulacak
         Yapı olarak yetkiler yapısına benzeyecek.
         AnaGrup taki satırlar Kioskta ilk görünecekler, OrtaGrup takiler seçilen AnaGrup a göre gelecek, AltGrup takiler
         ise yine seçilen OrtaGrup a göre gelecek.
        */
        [Key]
        public int Id { get; set; }

        /* AnaGrup, OrtaGrup, AltGrup olarak 3 farklı grup verilebilir, BankoGrupları ndan beslenir. */
        public required BankoGrup BankoGrup { get; set; }

        /* AnaGrup değilse UstIslemId yi eklemek mecburi(-1 demek AnaGrup demektir, kiosk ta ilk görünür), 
        bu veri yine seviyesine göre eklenecek */
        public int BankoUstIslemId { get; set; } = -1;
        public required string BankoIslemAdı { get; set; }
        public int BankoIslemSira { get; set; }
        public Aktiflik BankoIslemAktiflik { get; set; }
        public string? DiffLang { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }

    
}
