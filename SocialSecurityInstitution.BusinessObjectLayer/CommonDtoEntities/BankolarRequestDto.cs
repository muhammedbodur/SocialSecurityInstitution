using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialSecurityInstitution.BusinessObjectLayer.ValidationAttributes;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    public class BankolarRequestDto
    {
        [PositiveNumber(AllowZero = true)]
        public int BankoId { get; set; }

        [Range(1, 9999, ErrorMessage = "Banko No 1-9999 arasında olmalıdır")]
        public int BankoNo { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [TcKimlikNoValidation]
        public string TcKimlikNo { get; set; }

        [Range(1, 999999, ErrorMessage = "Sicil No 1-999999 arasında olmalıdır")]
        public int SicilNo { get; set; }

        [Required(ErrorMessage = "Personel Ad Soyad zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Personel Ad Soyad 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public string PersonelAdSoyad { get; set; }

        [StringLength(50, ErrorMessage = "Personel NickName 50 karakterden fazla olamaz")]
        [RegularExpression(@"^[a-zA-Z0-9_]*$", ErrorMessage = "NickName sadece harf, rakam ve _ içerebilir")]
        public string PersonelNickName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Personel Departman seçiniz")]
        public int PersonelDepartmanId { get; set; }

        [Required(ErrorMessage = "Personel Departman Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Personel Departman Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string PersonelDepartmanAdi { get; set; }

        [StringLength(500, ErrorMessage = "Personel Resim yolu 500 karakterden fazla olamaz")]
        public string PersonelResim { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Departman seçiniz")]
        public int DepartmanId { get; set; }

        [Required(ErrorMessage = "Departman Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Departman Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string DepartmanAdi { get; set; }

        [Required(ErrorMessage = "Departman Aktiflik durumu seçimi zorunludur")]
        public Aktiflik DepartmanAktiflik { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Hizmet Binası seçiniz")]
        public int HizmetBinasiId { get; set; }

        [Required(ErrorMessage = "Hizmet Binası Adı zorunludur")]
        [StringLength(100, ErrorMessage = "Hizmet Binası Adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string HizmetBinasiAdi { get; set; }

        [Required(ErrorMessage = "Kat Tipi seçimi zorunludur")]
        public KatTipi KatTipi { get; set; }

        [Required(ErrorMessage = "Hizmet Binası Aktiflik durumu seçimi zorunludur")]
        public Aktiflik HizmetBinasiAktiflik { get; set; }

        [Required(ErrorMessage = "Banko Aktiflik durumu seçimi zorunludur")]
        public Aktiflik BankoAktiflik { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BankoEklenmeTarihi { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BankoDuzenlenmeTarihi { get; set; }
    }
}
