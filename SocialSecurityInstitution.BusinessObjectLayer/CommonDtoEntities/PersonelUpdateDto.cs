using System;
using System.ComponentModel.DataAnnotations;
using static SocialSecurityInstitution.BusinessObjectLayer.CommonEntities.Enums;

namespace SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities
{
    /// <summary>
    /// Personel güncelleme işlemleri için özel DTO
    /// Sadece güncellenebilir alanları içerir
    /// </summary>
    public class PersonelUpdateDto
    {
        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        public required string TcKimlikNo { get; set; }

        // TAB 1: PERSONEL BİLGİLERİ
        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir")]
        public required string AdSoyad { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir sicil numarası giriniz")]
        public int SicilNo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Departman seçimi zorunludur")]
        public int DepartmanId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Servis seçimi zorunludur")]
        public int ServisId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Ünvan seçimi zorunludur")]
        public int UnvanId { get; set; }

        [StringLength(200, ErrorMessage = "Görev bilgisi en fazla 200 karakter olabilir")]
        public string? Gorev { get; set; }

        [StringLength(200, ErrorMessage = "Uzmanlık bilgisi en fazla 200 karakter olabilir")]
        public string? Uzmanlik { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Atanma nedeni seçimi zorunludur")]
        public int AtanmaNedeniId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Hizmet binası seçimi zorunludur")]
        public int HizmetBinasiId { get; set; }

        public PersonelAktiflikDurum PersonelAktiflikDurum { get; set; }

        // TAB 2: İLETİŞİM BİLGİLERİ
        [Required(ErrorMessage = "E-mail adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz")]
        [StringLength(100, ErrorMessage = "E-mail adresi en fazla 100 karakter olabilir")]
        public required string Email { get; set; }

        [Range(0, 9999, ErrorMessage = "Dahili telefon 0-9999 arasında olmalıdır")]
        public int Dahili { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(15, ErrorMessage = "Telefon numarası en fazla 15 karakter olabilir")]
        public string? CepTelefonu { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(15, ErrorMessage = "Telefon numarası en fazla 15 karakter olabilir")]
        public string? CepTelefonu2 { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [StringLength(15, ErrorMessage = "Telefon numarası en fazla 15 karakter olabilir")]
        public string? EvTelefonu { get; set; }

        [StringLength(500, ErrorMessage = "Adres bilgisi en fazla 500 karakter olabilir")]
        public string? Adres { get; set; }

        // Foreign Key - Nullable yapıyoruz validation için
        public int? IlId { get; set; }
        public int? IlceId { get; set; }

        [StringLength(100, ErrorMessage = "Semt bilgisi en fazla 100 karakter olabilir")]
        public string? Semt { get; set; }

        // TAB 3: KİŞİSEL BİLGİLER
        [Required(ErrorMessage = "Doğum tarihi zorunludur")]
        public DateTime DogumTarihi { get; set; }

        public Cinsiyet Cinsiyet { get; set; }
        public MedeniDurumu MedeniDurumu { get; set; }
        public KanGrubu KanGrubu { get; set; }
        public EvDurumu EvDurumu { get; set; }

        public int UlasimServis1 { get; set; } = 0;
        public int UlasimServis2 { get; set; } = 0;
        public int Tabldot { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Geçerli bir kart numarası giriniz")]
        public int KartNo { get; set; }

        public DateTime? KartNoAktiflikTarihi { get; set; }

        // TAB 4: ÖZLÜK BİLGİLERİ
        [StringLength(50, ErrorMessage = "Emekli sicil no en fazla 50 karakter olabilir")]
        public string? EmekliSicilNo { get; set; }

        public OgrenimDurumu OgrenimDurumu { get; set; }

        [StringLength(200, ErrorMessage = "Okul adı en fazla 200 karakter olabilir")]
        public string? BitirdigiOkul { get; set; }

        [StringLength(200, ErrorMessage = "Bölüm adı en fazla 200 karakter olabilir")]
        public string? BitirdigiBolum { get; set; }

        [Range(0, 15, ErrorMessage = "Öğrenim süresi 0-15 yıl arasında olmalıdır")]
        public int OgrenimSuresi { get; set; }

        [StringLength(100, ErrorMessage = "Branş bilgisi en fazla 100 karakter olabilir")]
        public string? Bransi { get; set; }

        // Foreign Key - Nullable
        public int? SendikaId { get; set; }

        public SehitYakinligi SehitYakinligi { get; set; }

        // TAB 5: EŞ VE ÇOCUK BİLGİLERİ
        [StringLength(100, ErrorMessage = "Eş adı en fazla 100 karakter olabilir")]
        public string? EsininAdi { get; set; }

        public EsininIsDurumu EsininIsDurumu { get; set; } = EsininIsDurumu.belirtilmemis;

        [StringLength(100, ErrorMessage = "Eş ünvanı en fazla 100 karakter olabilir")]
        public string? EsininUnvani { get; set; }

        [StringLength(500, ErrorMessage = "Eş iş adresi en fazla 500 karakter olabilir")]
        public string? EsininIsAdresi { get; set; }

        // Foreign Key - Nullable
        public int? EsininIsIlId { get; set; }
        public int? EsininIsIlceId { get; set; }

        [StringLength(100, ErrorMessage = "Eş iş semt bilgisi en fazla 100 karakter olabilir")]
        public string? EsininIsSemt { get; set; }

        // TAB 6-10: DİĞER BİLGİLER
        [StringLength(1000, ErrorMessage = "Hizmet bilgisi en fazla 1000 karakter olabilir")]
        public string? HizmetBilgisi { get; set; }

        [StringLength(1000, ErrorMessage = "Eğitim bilgisi en fazla 1000 karakter olabilir")]
        public string? EgitimBilgisi { get; set; }

        [StringLength(1000, ErrorMessage = "İmza yetkileri en fazla 1000 karakter olabilir")]
        public string? ImzaYetkileri { get; set; }

        [StringLength(1000, ErrorMessage = "Ceza bilgileri en fazla 1000 karakter olabilir")]
        public string? CezaBilgileri { get; set; }

        [StringLength(1000, ErrorMessage = "Engel bilgileri en fazla 1000 karakter olabilir")]
        public string? EngelBilgileri { get; set; }

        [StringLength(255, ErrorMessage = "Resim yolu en fazla 255 karakter olabilir")]
        public string? Resim { get; set; }

        // Otomatik doldurulan alanlar - validation gerektirmez
        public DateTime DuzenlenmeTarihi { get; set; } = DateTime.Now;
    }
}