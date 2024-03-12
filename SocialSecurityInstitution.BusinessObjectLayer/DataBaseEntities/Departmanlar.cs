using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer
{
    public class Departmanlar
    {
        [Key]
        public int Id { get; set; }
        public required string DepartmanAdi { get; set; }

        /* DepartmanAktiflikDurum parametresi Departmanın Aktif edilip edilmemesini belirtmekte */
        public Aktiflik DepartmanAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<Personeller>? Personeller_ { get; set; }
        public ICollection<Bankolar>? Bankolar_ { get; set; }
    }   
}
