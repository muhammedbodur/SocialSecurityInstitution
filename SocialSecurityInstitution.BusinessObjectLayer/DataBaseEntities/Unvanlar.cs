using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Unvanlar
    {
        [Key]
        public int UnvanId { get; set; }
        public required string UnvanAdi { get; set; }

        /* UnvanAktiflikDurum parametresi Unvanın Aktif edilip edilmemesini belirtmekte */
        public Aktiflik UnvanAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<Personeller>? Personeller_ { get; set; }
    }
}
