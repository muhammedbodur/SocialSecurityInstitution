using System;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class KanalPersonelleriViewRequestDto
    {
        public int KanalPersonelId { get; set; }
        public int KanalAltIslemId { get; set; }
        public string KanalAltAdi { get; set; }
        public int KanalIslemId { get; set; }
        public string KanalIslemAdi { get; set; }
        public string TcKimlikNo { get; set; }
        public int SicilNo { get; set; }
        public string AdSoyad { get; set; }
        public int HizmetBinasiId { get; set; }
        public string HizmetBinasiAdi { get; set; }
        public int DepartmanId { get; set; }
        public string DepartmanAdi { get; set; }
        public int ServisId { get; set; }             
        public string ServisAdi { get; set; }         
        public int UnvanId { get; set; }              
        public string UnvanAdi { get; set; }          
        public Aktiflik Aktiflik { get; set; }
        public string AktiflikAdi { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime? DuzenlenmeTarihi { get; set; }
    }
}