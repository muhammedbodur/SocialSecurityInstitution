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
    public class PersonelRequestDto
    {
        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [TcKimlikNoValidation]
        public required string TcKimlikNo { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır")]
        [TurkishText]
        public required string AdSoyad { get; set; }

        [StringLength(50, ErrorMessage = "NickName 50 karakterden fazla olamaz")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "NickName sadece harf, rakam ve _ içerebilir")]
        public string? NickName { get; set; }

        [Range(1, 999999, ErrorMessage = "Sicil No 1-999999 arasında olmalıdır")]
        public int SicilNo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Departman seçiniz")]
        public int DepartmanId { get; set; }

        [StringLength(100, ErrorMessage = "Departman adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? DepartmanAdi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Servis seçiniz")]
        public int ServisId { get; set; }

        [StringLength(100, ErrorMessage = "Servis adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? ServisAdi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir Unvan seçiniz")]
        public int UnvanId { get; set; }

        [StringLength(100, ErrorMessage = "Unvan adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? UnvanAdi { get; set; }

        [StringLength(200, ErrorMessage = "Görev 200 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? Gorev { get; set; }

        [StringLength(200, ErrorMessage = "Uzmanlık 200 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? Uzmanlik { get; set; }

        [StringLength(300, ErrorMessage = "Atanma nedeni 300 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? AtanmaNedeni { get; set; }

        [StringLength(100, ErrorMessage = "Hizmet binası 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? HizmetBinasi { get; set; }

        [Required(ErrorMessage = "Personel tipi seçimi zorunludur")]
        public PersonelTipi PersonelTipi { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [StringLength(100, ErrorMessage = "Email 100 karakterden fazla olamaz")]
        public string? Email { get; set; }

        [Range(1000, 9999, ErrorMessage = "Dahili numara 4 haneli olmalıdır")]
        public int Dahili { get; set; }

        [TurkishPhoneValidation(PhoneType = PhoneType.Mobile)]
        public string? CepTelefonu { get; set; }

        [TurkishPhoneValidation(PhoneType = PhoneType.Mobile)]
        public string? CepTelefonu2 { get; set; }

        [TurkishPhoneValidation(PhoneType = PhoneType.Landline)]
        public string? EvTelefonu { get; set; }

        [StringLength(500, ErrorMessage = "Adres 500 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? Adres { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int IlId { get; set; }

        [StringLength(50, ErrorMessage = "İl adı 50 karakterden fazla olamaz")]
        [TurkishText]
        public string? IlAdi { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int IlceId { get; set; }

        [StringLength(50, ErrorMessage = "İlçe adı 50 karakterden fazla olamaz")]
        [TurkishText]
        public string? IlceAdi { get; set; }

        [StringLength(100, ErrorMessage = "Semt 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? Semt { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur")]
        [DataType(DataType.Date)]
        [AgeValidation(MinAge = 18, MaxAge = 65)]
        public DateTime DogumTarihi { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçimi zorunludur")]
        public Cinsiyet Cinsiyet { get; set; }

        [Required(ErrorMessage = "Medeni durum seçimi zorunludur")]
        public MedeniDurumu MedeniDurumu { get; set; }

        [Required(ErrorMessage = "Kan grubu seçimi zorunludur")]
        public KanGrubu KanGrubu { get; set; }

        [Required(ErrorMessage = "Ev durumu seçimi zorunludur")]
        public EvDurumu EvDurumu { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int UlasimServis1 { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int UlasimServis2 { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int Tabldot { get; set; }

        [Required(ErrorMessage = "Personel aktiflik durumu seçimi zorunludur")]
        public PersonelAktiflikDurum PersonelAktiflikDurum { get; set; }

        [StringLength(20, ErrorMessage = "Emekli sicil no 20 karakterden fazla olamaz")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Emekli sicil no sadece rakam içerebilir")]
        public string? EmekliSicilNo { get; set; }

        [Required(ErrorMessage = "Öğrenim durumu seçimi zorunludur")]
        public OgrenimDurumu OgrenimDurumu { get; set; }

        [StringLength(200, ErrorMessage = "Bitirdiği okul 200 karakterden fazla olamaz")]
        [TurkishText]
        public string? BitirdigiOkul { get; set; }

        [StringLength(200, ErrorMessage = "Bitirdiği bölüm 200 karakterden fazla olamaz")]
        [TurkishText]
        public string? BitirdigiBolum { get; set; }

        [Range(0, 50, ErrorMessage = "Öğrenim süresi 0-50 yıl arasında olmalıdır")]
        public int OgrenimSuresi { get; set; }

        [StringLength(100, ErrorMessage = "Branş 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? Bransi { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int SendikaId { get; set; }

        [StringLength(100, ErrorMessage = "Sendika adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? SendikaAdi { get; set; }

        [Required(ErrorMessage = "Şehit yakınlığı seçimi zorunludur")]
        public SehitYakinligi SehitYakinligi { get; set; }

        [StringLength(100, ErrorMessage = "Eşinin adı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? EsininAdi { get; set; }

        [Required(ErrorMessage = "Eşinin iş durumu seçimi zorunludur")]
        public EsininIsDurumu EsininIsDurumu { get; set; }

        [StringLength(100, ErrorMessage = "Eşinin unvanı 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? EsininUnvani { get; set; }

        [StringLength(500, ErrorMessage = "Eşinin iş adresi 500 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? EsininIsAdresi { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int EsininIsIlId { get; set; }

        [StringLength(50, ErrorMessage = "Eşinin iş ili 50 karakterden fazla olamaz")]
        [TurkishText]
        public string? EsininIsIlAdi { get; set; }

        [PositiveNumber(AllowZero = true)]
        public int EsininIsIlceId { get; set; }

        [StringLength(50, ErrorMessage = "Eşinin iş ilçesi 50 karakterden fazla olamaz")]
        [TurkishText]
        public string? EsininIsIlceAdi { get; set; }

        [StringLength(100, ErrorMessage = "Eşinin iş semti 100 karakterden fazla olamaz")]
        [TurkishText]
        public string? EsininIsSemt { get; set; }

        [StringLength(1000, ErrorMessage = "Hizmet bilgisi 1000 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? HizmetBilgisi { get; set; }

        [StringLength(1000, ErrorMessage = "Eğitim bilgisi 1000 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? EgitimBilgisi { get; set; }

        [StringLength(1000, ErrorMessage = "İmza yetkileri 1000 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? ImzaYetkileri { get; set; }

        [StringLength(1000, ErrorMessage = "Ceza bilgileri 1000 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? CezaBilgileri { get; set; }

        [StringLength(1000, ErrorMessage = "Engel bilgileri 1000 karakterden fazla olamaz")]
        [NoHtmlContent]
        public string? EngelBilgileri { get; set; }

        [StringLength(500, ErrorMessage = "Resim yolu 500 karakterden fazla olamaz")]
        public string? Resim { get; set; }

        [StringLength(100, ErrorMessage = "Connection ID 100 karakterden fazla olamaz")]
        public string? ConnectionId { get; set; }

        public ConnectionStatus ConnectionStatus { get; set; }

        public DateTime DuzenlenmeTarihi { get; set; }
    }
}
