using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class HizmetBinalariDto
    {
        public int HizmetBinasiId { get; set; }
        public required string HizmetBinasiAdi { get; set; }
        public string? Aciklama { get; set; }
        public int DepartmanId { get; set; }
        [ForeignKey("DepartmanId")]
        public DepartmanlarDto Departman { get; set; }
        public Aktiflik HizmetBinasiAktiflik { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }

        public ICollection<BankolarDto>? Bankolar { get; set; }
        public ICollection<PersonellerDto>? Personeller { get; set; }
    }
}
