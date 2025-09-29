# Business Object Layer (İş Nesneleri Katmanı) Şeması

## 📁 Klasör Yapısı

```
SocialSecurityInstitution.BusinessObjectLayer/
├── 📁 CommonDtoEntities/ (63 dosya)
│   ├── 📄 AtanmaNedenleriDto.cs
│   ├── 📄 BankolarDto.cs
│   ├── 📄 DepartmanlarDto.cs
│   ├── 📄 HizmetBinalariDto.cs
│   ├── 📄 KanallarDto.cs
│   ├── 📄 KanallarAltDto.cs
│   ├── 📄 KanalIslemleriDto.cs
│   ├── 📄 KanalAltIslemleriDto.cs
│   ├── 📄 KanalPersonelleriDto.cs
│   ├── 📄 PersonellerDto.cs
│   ├── 📄 ServislerDto.cs
│   ├── 📄 SiralarDto.cs
│   ├── 📄 UnvanlarDto.cs
│   ├── 📄 YetkilerDto.cs
│   ├── 📄 LoginDto.cs
│   ├── 📄 UserInfoDto.cs
│   ├── 📄 BankoCustomDto.cs
│   ├── 📄 KanalCustomDto.cs
│   ├── 📄 PersonelCustomDto.cs
│   ├── 📄 ReportDto.cs
│   └── ... (43 adet daha)
├── 📁 DataBaseEntities/ (34 dosya)
│   ├── 📄 AtanmaNedenleri.cs
│   ├── 📄 Bankolar.cs
│   ├── 📄 Departmanlar.cs
│   ├── 📄 HizmetBinalari.cs
│   ├── 📄 Kanallar.cs
│   ├── 📄 KanallarAlt.cs
│   ├── 📄 KanalIslemleri.cs
│   ├── 📄 KanalAltIslemleri.cs
│   ├── 📄 KanalPersonelleri.cs
│   ├── 📄 Personeller.cs
│   ├── 📄 Servisler.cs
│   ├── 📄 Siralar.cs
│   ├── 📄 Unvanlar.cs
│   ├── 📄 Yetkiler.cs
│   └── ... (20 adet daha)
├── 📁 CommonEntities/ (1 dosya)
│   └── 📄 Enums.cs
├── 📁 Extensions/ (1 dosya)
│   └── 📄 Extensions.cs
└── 📄 SocialSecurityInstitution.BusinessObjectLayer.csproj
```

## 🎯 Katman Amacı

Bu katman, **veri transfer nesneleri (DTO)** ve **veritabanı entity'leri** arasında köprü görevi görür:

1. **Data Transfer Objects (DTO)**: Katmanlar arası veri taşıma
2. **Database Entities**: Veritabanı tablolarının C# karşılıkları
3. **Common Entities**: Ortak kullanılan enum'lar ve sabitler
4. **Extensions**: Yardımcı genişletme methodları

## 📊 DTO Entities (63 Dosya)

### Ana DTO Kategorileri:

#### 1. Temel CRUD DTO'ları
```csharp
public class KanallarDto
{
    public int KanalId { get; set; }
    public string KanalAdi { get; set; }
    public string KanalAciklamasi { get; set; }
    public int DepartmanId { get; set; }
    public bool Aktif { get; set; }
    public DateTime OlusturmaTarihi { get; set; }
    public DateTime? GuncellemeTarihi { get; set; }
    public string OlusturanKullanici { get; set; }
    public string GuncelleyenKullanici { get; set; }
}

public class PersonellerDto
{
    public int PersonelId { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string TcKimlikNo { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public int DepartmanId { get; set; }
    public int UnvanId { get; set; }
    public bool Aktif { get; set; }
    public DateTime IseBaslamaTarihi { get; set; }
    public DateTime? IstenAyrılmaTarihi { get; set; }
}

public class BankolarDto
{
    public int BankoId { get; set; }
    public string BankoAdi { get; set; }
    public int HizmetBinasiId { get; set; }
    public int PersonelId { get; set; }
    public bool Aktif { get; set; }
    public int MaksimumSiraSayisi { get; set; }
    public TimeSpan CalismaSaatiBaslangic { get; set; }
    public TimeSpan CalismaSaatiBitis { get; set; }
}
```

#### 2. Custom DTO'lar (Özel İşlemler İçin)
```csharp
public class BankoCustomDto
{
    public int BankoId { get; set; }
    public string BankoAdi { get; set; }
    public string PersonelAdSoyad { get; set; }
    public int BekleyenSiraSayisi { get; set; }
    public int TamamlananSiraSayisi { get; set; }
    public TimeSpan OrtalamaBeklemeZamani { get; set; }
    public string Durum { get; set; }
}

public class KanalCustomDto
{
    public int KanalId { get; set; }
    public string KanalAdi { get; set; }
    public string DepartmanAdi { get; set; }
    public int ToplamIslemSayisi { get; set; }
    public int AktifIslemSayisi { get; set; }
    public int AtanmisPersonelSayisi { get; set; }
    public List<KanalIslemleriDto> KanalIslemleri { get; set; }
}

public class PersonelCustomDto
{
    public int PersonelId { get; set; }
    public string AdSoyad { get; set; }
    public string DepartmanAdi { get; set; }
    public string UnvanAdi { get; set; }
    public List<string> Yetkiler { get; set; }
    public List<string> AtanmisKanallar { get; set; }
    public int AylikIslemSayisi { get; set; }
    public decimal PerformansPuani { get; set; }
}
```

#### 3. Authentication & Authorization DTO'ları
```csharp
public class LoginDto
{
    public string KullaniciAdi { get; set; }
    public string Sifre { get; set; }
    public bool BeniHatirla { get; set; }
}

public class UserInfoDto
{
    public int PersonelId { get; set; }
    public string AdSoyad { get; set; }
    public string Email { get; set; }
    public string DepartmanAdi { get; set; }
    public string UnvanAdi { get; set; }
    public List<string> Yetkiler { get; set; }
    public List<string> MenuYetkileri { get; set; }
    public DateTime SonGirisTarihi { get; set; }
}
```

#### 4. Reporting DTO'ları
```csharp
public class ReportDto
{
    public string ReportTitle { get; set; }
    public DateTime ReportDate { get; set; }
    public string ReportType { get; set; }
    public Dictionary<string, object> Parameters { get; set; }
    public List<Dictionary<string, object>> Data { get; set; }
}

public class DashboardDto
{
    public int ToplamPersonelSayisi { get; set; }
    public int AktifBankoSayisi { get; set; }
    public int BekleyenSiraSayisi { get; set; }
    public int BugunTamamlananIslemSayisi { get; set; }
    public List<BankoCustomDto> BankoListesi { get; set; }
    public List<KanalCustomDto> KanalListesi { get; set; }
}
```

## 🗄️ Database Entities (34 Dosya)

### Ana Entity Kategorileri:

#### 1. Organizasyon Entities
```csharp
[Table("HizmetBinalari")]
public class HizmetBinalari
{
    [Key]
    public int HizmetBinasiId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string HizmetBinasiAdi { get; set; }
    
    [StringLength(200)]
    public string Adres { get; set; }
    
    [StringLength(20)]
    public string Telefon { get; set; }
    
    public bool Aktif { get; set; }
    
    // Navigation Properties
    public virtual ICollection<Departmanlar> Departmanlar { get; set; }
    public virtual ICollection<Bankolar> Bankolar { get; set; }
}

[Table("Departmanlar")]
public class Departmanlar
{
    [Key]
    public int DepartmanId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string DepartmanAdi { get; set; }
    
    [StringLength(500)]
    public string Aciklama { get; set; }
    
    public int HizmetBinasiId { get; set; }
    public bool Aktif { get; set; }
    
    // Foreign Keys
    [ForeignKey("HizmetBinasiId")]
    public virtual HizmetBinalari HizmetBinasi { get; set; }
    
    // Navigation Properties
    public virtual ICollection<Personeller> Personeller { get; set; }
    public virtual ICollection<Kanallar> Kanallar { get; set; }
}
```

#### 2. Kanal Sistemi Entities
```csharp
[Table("Kanallar")]
public class Kanallar
{
    [Key]
    public int KanalId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string KanalAdi { get; set; }
    
    [StringLength(500)]
    public string KanalAciklamasi { get; set; }
    
    public int DepartmanId { get; set; }
    public bool Aktif { get; set; }
    public DateTime OlusturmaTarihi { get; set; }
    public DateTime? GuncellemeTarihi { get; set; }
    
    [StringLength(50)]
    public string OlusturanKullanici { get; set; }
    
    [StringLength(50)]
    public string GuncelleyenKullanici { get; set; }
    
    // Foreign Keys
    [ForeignKey("DepartmanId")]
    public virtual Departmanlar Departman { get; set; }
    
    // Navigation Properties
    public virtual ICollection<KanallarAlt> KanallarAlt { get; set; }
    public virtual ICollection<KanalIslemleri> KanalIslemleri { get; set; }
}

[Table("KanalIslemleri")]
public class KanalIslemleri
{
    [Key]
    public int KanalIslemId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string IslemAdi { get; set; }
    
    [StringLength(500)]
    public string IslemAciklamasi { get; set; }
    
    public int KanalId { get; set; }
    public bool Aktif { get; set; }
    public int Sira { get; set; }
    
    // Foreign Keys
    [ForeignKey("KanalId")]
    public virtual Kanallar Kanal { get; set; }
    
    // Navigation Properties
    public virtual ICollection<KanalAltIslemleri> KanalAltIslemleri { get; set; }
}
```

#### 3. Personel ve Banko Entities
```csharp
[Table("Personeller")]
public class Personeller
{
    [Key]
    public int PersonelId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Ad { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Soyad { get; set; }
    
    [Required]
    [StringLength(11)]
    public string TcKimlikNo { get; set; }
    
    [StringLength(100)]
    public string Email { get; set; }
    
    [StringLength(20)]
    public string Telefon { get; set; }
    
    public int DepartmanId { get; set; }
    public int UnvanId { get; set; }
    public bool Aktif { get; set; }
    public DateTime IseBaslamaTarihi { get; set; }
    public DateTime? IstenAyrılmaTarihi { get; set; }
    
    // Foreign Keys
    [ForeignKey("DepartmanId")]
    public virtual Departmanlar Departman { get; set; }
    
    [ForeignKey("UnvanId")]
    public virtual Unvanlar Unvan { get; set; }
    
    // Navigation Properties
    public virtual ICollection<Bankolar> Bankolar { get; set; }
    public virtual ICollection<KanalPersonelleri> KanalPersonelleri { get; set; }
}

[Table("Bankolar")]
public class Bankolar
{
    [Key]
    public int BankoId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string BankoAdi { get; set; }
    
    public int HizmetBinasiId { get; set; }
    public int PersonelId { get; set; }
    public bool Aktif { get; set; }
    public int MaksimumSiraSayisi { get; set; }
    public TimeSpan CalismaSaatiBaslangic { get; set; }
    public TimeSpan CalismaSaatiBitis { get; set; }
    
    // Foreign Keys
    [ForeignKey("HizmetBinasiId")]
    public virtual HizmetBinalari HizmetBinasi { get; set; }
    
    [ForeignKey("PersonelId")]
    public virtual Personeller Personel { get; set; }
    
    // Navigation Properties
    public virtual ICollection<Siralar> Siralar { get; set; }
}
```

## 🔧 Common Entities

### Enums.cs
```csharp
public static class Enums
{
    public enum SiraDurumu
    {
        Bekliyor = 1,
        Cagrildi = 2,
        Isleniyor = 3,
        Tamamlandi = 4,
        Iptal = 5
    }
    
    public enum PersonelDurumu
    {
        Aktif = 1,
        Pasif = 2,
        Izinli = 3,
        Ayrılmis = 4
    }
    
    public enum BankoDurumu
    {
        Acik = 1,
        Kapali = 2,
        Mola = 3,
        Bakim = 4
    }
    
    public enum YetkiTuru
    {
        Okuma = 1,
        Yazma = 2,
        Guncelleme = 3,
        Silme = 4,
        Admin = 5
    }
    
    public enum IslemTuru
    {
        Emeklilik = 1,
        SaglikSigortasi = 2,
        IssizlikSigortasi = 3,
        MeslekHastaligi = 4,
        IsKazasi = 5,
        AnnelikSigortasi = 6
    }
}
```

## 🔧 Extensions

### Extensions.cs
```csharp
public static class Extensions
{
    public static string ToFullName(this Personeller personel)
    {
        return $"{personel.Ad} {personel.Soyad}";
    }
    
    public static string ToFormattedDate(this DateTime date)
    {
        return date.ToString("dd.MM.yyyy HH:mm");
    }
    
    public static string ToTurkishCurrency(this decimal amount)
    {
        return amount.ToString("C", new CultureInfo("tr-TR"));
    }
    
    public static bool IsWorkingHours(this TimeSpan time, TimeSpan start, TimeSpan end)
    {
        return time >= start && time <= end;
    }
    
    public static string ToStatusText(this SiraDurumu durum)
    {
        return durum switch
        {
            SiraDurumu.Bekliyor => "Bekliyor",
            SiraDurumu.Cagrildi => "Çağrıldı",
            SiraDurumu.Isleniyor => "İşleniyor",
            SiraDurumu.Tamamlandi => "Tamamlandı",
            SiraDurumu.Iptal => "İptal",
            _ => "Bilinmeyen"
        };
    }
}
```

## 🎯 DTO vs Entity Mapping

### AutoMapper Profilleri
```csharp
// KanallarDto ↔ Kanallar
CreateMap<Kanallar, KanallarDto>()
    .ForMember(dest => dest.DepartmanAdi, 
               opt => opt.MapFrom(src => src.Departman.DepartmanAdi));

CreateMap<KanallarDto, Kanallar>()
    .ForMember(dest => dest.Departman, opt => opt.Ignore());

// PersonellerDto ↔ Personeller
CreateMap<Personeller, PersonellerDto>()
    .ForMember(dest => dest.DepartmanAdi, 
               opt => opt.MapFrom(src => src.Departman.DepartmanAdi))
    .ForMember(dest => dest.UnvanAdi, 
               opt => opt.MapFrom(src => src.Unvan.UnvanAdi));

CreateMap<PersonellerDto, Personeller>()
    .ForMember(dest => dest.Departman, opt => opt.Ignore())
    .ForMember(dest => dest.Unvan, opt => opt.Ignore());
```

## 📊 Veri Akış Şeması

```
Database Tables
       ↓
Entity Framework Entities
       ↓
AutoMapper
       ↓
DTO Objects
       ↓
Business Logic Layer
       ↓
Presentation Layer
       ↓
JSON/Views
```

## 🔐 Validation Attributes

### Entity Validations
```csharp
[Required(ErrorMessage = "Kanal adı zorunludur")]
[StringLength(100, ErrorMessage = "Kanal adı en fazla 100 karakter olabilir")]
public string KanalAdi { get; set; }

[Required(ErrorMessage = "TC Kimlik No zorunludur")]
[StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır")]
[RegularExpression(@"^\d{11}$", ErrorMessage = "TC Kimlik No sadece rakam içermelidir")]
public string TcKimlikNo { get; set; }

[EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
public string Email { get; set; }

[Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
public string Telefon { get; set; }
```

Bu katman, **veri bütünlüğü** ve **tip güvenliği** sağlayarak uygulamanın temelini oluşturur.
