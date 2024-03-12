using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Bankolar
    {
        [Key]
        public int Id { get; set; }
        public int DepartmanId { get; set; }
        public required Departmanlar Departman { get; set; }
        public int BankoNo { get; set; }

        /* BankolarAktiflikDurum parametresi Bankoların Aktif edilip edilmemesini belirtmekte */
        public Aktiflik BankoAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<BankolarKullanici>? BankolarKullanici_ { get; set; }
    } 
}
