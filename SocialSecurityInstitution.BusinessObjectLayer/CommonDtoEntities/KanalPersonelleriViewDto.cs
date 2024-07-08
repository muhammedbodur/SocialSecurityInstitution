using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalPersonelleriViewDto
    {
        public required string TcKimlikNo { get; set; }
        public int SicilNo { get; set; }
        public required string AdSoyad { get; set; }
        public int DepartmanId { get; set; }
        public required string DepartmanAdi { get; set; }
        public int ServisId { get; set; }
        public required string ServisAdi { get; set; }
        public int UnvanId { get; set; }
        public required string UnvanAdi{ get; set; }
        public string? Gorev { get; set; }
        public string? Uzmanlik { get; set; }
        public string? Resim { get; set; }
        public List<KanalIslemleriDto>? KanalIslemleri { get; set; }
        public List<KanalAltIslemleriDto>? KanalAltIslemleri { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
