using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
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
        public int DepartmanId { get; set; }
        [Required]
        public string DepartmanAdi { get; set; }
        public Aktiflik DepartmanAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;

        public ICollection<Personeller>? Personeller { get; set; }
        public ICollection<HizmetBinalari>? HizmetBinalari { get; set; }
    }
}
