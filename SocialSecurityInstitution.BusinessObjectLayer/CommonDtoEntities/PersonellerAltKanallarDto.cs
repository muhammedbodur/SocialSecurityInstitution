using System;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class PersonellerAltKanallarDto
    {
        public int KanalAltIslemId { get; set; }
        public string KanalAltAdi { get; set; }
        public int KanalIslemId { get; set; }
        public string KanalIslemAdi { get; set; }
        public string TcKimlikNo { get; set; }
        public string AdSoyad { get; set; }
        public int HizmetBinasiId { get; set; }
        public string HizmetBinasiAdi { get; set; }
        public int DepartmanId { get; set; }
        public string DepartmanAdi { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime? DuzenlenmeTarihi { get; set; }
    }
}
