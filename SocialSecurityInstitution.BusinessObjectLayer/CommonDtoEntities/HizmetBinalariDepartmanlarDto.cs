using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class HizmetBinalariDepartmanlarDto
    {
        public int HizmetBinasiId { get; set; }
        public required string HizmetBinasiAdi { get; set; }
        public int DepartmanId { get; set; }
        [ForeignKey("DepartmanId")]
        public DepartmanlarDto Departman { get; set; }
        public Aktiflik HizmetBinasiAktiflik { get; set; }
        public DateTime HizmetBinasiEklenmeTarihi { get; set; }
        public DateTime HizmetBinasiDuzenlenmeTarihi { get; set; }
        public required string DepartmanAdi { get; set; }
        public Aktiflik DepartmanAktiflik { get; set; }
        public DateTime DepartmanEklenmeTarihi { get; set; }
        public DateTime DepartmanDuzenlenmeTarihi { get; set; }
    }
}
