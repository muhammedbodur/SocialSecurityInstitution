using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class BankolarRequestDto
    {
        public int BankoId { get; set; }
        public int BankoNo { get; set; }
        public string TcKimlikNo { get; set; }
        public int SicilNo { get; set; }
        public string PersonelAdSoyad { get; set; }
        public string PersonelNickName { get; set; }
        public int PersonelDepartmanId { get; set; }
        public string PersonelDepartmanAdi { get; set; }
        public string PersonelResim { get; set; }
        public int DepartmanId { get; set; }
        public string DepartmanAdi { get; set; }
        public Aktiflik DepartmanAktiflik { get; set; }
        public int HizmetBinasiId { get; set; }
        public string HizmetBinasiAdi { get; set; }
        public Aktiflik HizmetBinasiAktiflik { get; set; }
        public Aktiflik BankoAktiflik { get; set; }
        public DateTime BankoEklenmeTarihi { get; set; }
        public DateTime BankoDuzenlenmeTarihi { get; set; }
    }
}
