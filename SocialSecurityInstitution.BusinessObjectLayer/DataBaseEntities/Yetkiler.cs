using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Yetkiler
    {
        [Key]
        public int YetkiId { get; set; }

        // AnaYetki, OrtaYetki, AltYetki olarak 3 e ayrılır, Yetkiler ile YetkiTipleri arasında bir ilişki tanımlanıyor
        public required YetkiTurleri YetkiTuru { get; set; }
        public string YetkiAdi { get; set; } = null!;
        public string Aciklama { get; set; } = null!;

        /* AnaYetki değilse UstYetkiId yi eklemek mecburi(-1 demek AnaYetki demektir), bu veri yine seviyesine göre eklenecek */
        public int UstYetkiId { get; set; } = -1;

        /* YetkiAktiflik parametresi Yetkinin Aktif edilip edilmemesini belirtmekte */
        public Aktiflik YetkiAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<PersonelYetkileriii>? PersonelYetkileri_ { get; set; }
    }
}
