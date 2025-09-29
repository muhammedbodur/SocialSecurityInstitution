# ✅ Sıralı Düzeltme Listesi - Mimari Sorunlar

## 📋 Düzeltme Öncelik Sırası

Bu liste, tespit edilen mimari sorunları **öncelik sırasına** göre ve **sırayla düzeltilecek** şekilde organize edilmiştir.

---

## 🔴 ÖNCELIK 1: KRİTİK DÜZELTMELER (Hemen Başlanmalı)

### ✅ Düzeltme 1: Cookie'lerde Sensitive Data Güvenlik Açığını Düzelt
**Süre**: 1-2 gün  
**Zorluk**: 🟢 Kolay  
**Etki**: 🔴 Kritik (Güvenlik)

#### Etkilenen Dosyalar:
```
✅ LoginController.cs
```

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ - Sensitive data cookie'lerde
Response.Cookies.Append("TcKimlikNo", loginDto.TcKimlikNo, cookieOptions);
Response.Cookies.Append("Email", loginDto.Email, cookieOptions);
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU - Sadece JWT token cookie'de
Response.Cookies.Append("JwtToken", jwtToken, cookieOptions);
// Diğer bilgiler JWT içinde encrypted olarak saklanacak
```

---

### ✅ Düzeltme 2: Custom Service'lerden Context Dependency'sini Kaldır
**Süre**: 1-2 hafta  
**Zorluk**: 🟡 Orta  
**Etki**: 🔴 Kritik

#### Etkilenen Dosyalar:
```
✅ KanallarCustomService.cs
✅ BankolarCustomService.cs  
✅ PersonelCustomService.cs
✅ SiralarCustomService.cs
✅ KanalPersonelleriCustomService.cs
✅ HizmetBinalariCustomService.cs
✅ YetkilerCustomService.cs
✅ TvlerCustomService.cs
✅ KioskIslemGruplariCustomService.cs
✅ PersonelCocuklariCustomService.cs
✅ PersonelYetkileriCustomService.cs
✅ BankolarKullaniciCustomService.cs
✅ HubConnectionCustomService.cs
✅ HubTvConnectionCustomService.cs
```

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ
public class KanallarCustomService
{
    private readonly Context _context; // Kaldırılacak!
    private readonly IMapper _mapper;   // AutoMapper kalacak
    
    public KanallarCustomService(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU
public class KanallarCustomService
{
    private readonly IKanallarCustomRepository _repository; // Repository ekle
    private readonly IMapper _mapper; // AutoMapper kalsın
    
    public KanallarCustomService(IKanallarCustomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper; // AutoMapper kullanımı devam edecek
    }
}
```

---

### ✅ Düzeltme 3: Custom Repository'ler Oluştur
**Süre**: 2-3 hafta  
**Zorluk**: 🟡 Orta  
**Etki**: 🔴 Kritik

#### Oluşturulacak Repository'ler:
```
✅ IKanallarCustomRepository + KanallarCustomRepository
✅ IBankolarCustomRepository + BankolarCustomRepository
✅ IPersonelCustomRepository + PersonelCustomRepository
✅ ISiralarCustomRepository + SiralarCustomRepository
✅ IKanalPersonelleriCustomRepository + KanalPersonelleriCustomRepository
✅ IHizmetBinalariCustomRepository + HizmetBinalariCustomRepository
✅ IYetkilerCustomRepository + YetkilerCustomRepository
✅ ITvlerCustomRepository + TvlerCustomRepository
✅ IKioskIslemGruplariCustomRepository + KioskIslemGruplariCustomRepository
✅ IPersonelCocuklariCustomRepository + PersonelCocuklariCustomRepository
✅ IPersonelYetkileriCustomRepository + PersonelYetkileriCustomRepository
```

#### Örnek Repository:
```csharp
// ✅ Interface
public interface IKanallarCustomRepository
{
    Task<List<KanalAltIslemleri>> GetKanalAltIslemleriWithIncludesAsync();
    Task<List<KanalIslemleri>> GetKanalIslemleriByHizmetBinasiAsync(int hizmetBinasiId);
    Task<List<KanalPersonelleri>> GetKanalPersonelleriByHizmetBinasiAsync(int hizmetBinasiId);
}

// ✅ Implementation
public class KanallarCustomRepository : IKanallarCustomRepository
{
    private readonly Context _context; // Repository'de Context olacak
    
    public async Task<List<KanalAltIslemleri>> GetKanalAltIslemleriWithIncludesAsync()
    {
        return await _context.KanalAltIslemleri
            .Include(b => b.KanalIslem)
            .AsNoTracking()
            .ToListAsync();
    }
}
```

---

### ✅ Düzeltme 4: GenericRepository'yi Entity Döndürecek Şekilde Düzelt
**Süre**: 1 hafta  
**Zorluk**: 🟢 Kolay  
**Etki**: 🔴 Kritik

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ - DTO döndürüyor
public class GenericRepository<TEntity, TDto> : IGenericDal<TDto>
{
    public async Task<List<TDto>> GetAllAsync()
    {
        var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        return _mapper.Map<List<TDto>>(entities); // Mapping repository'de!
    }
}
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU - Entity döndürüyor
public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }
}

// Service'te AutoMapper kullanımı
public class KanallarService : IKanallarService
{
    public async Task<List<KanallarDto>> TGetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<List<KanallarDto>>(entities); // AutoMapper service'te
    }
}
```

---

---

### ✅ Düzeltme 5: Controller'lardaki God Object Anti-Pattern'ini Düzelt
**Süre**: 2-3 hafta  
**Zorluk**: 🟡 Orta  
**Etki**: 🔴 Kritik

#### Etkilenen Dosyalar:
```
✅ PersonelController.cs (14 dependency)
✅ ApiController.cs (7 dependency)
✅ TvController.cs (9 dependency)
✅ YetkilerController.cs (6 dependency)
```

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ - Çok fazla dependency (God Object)
public PersonelController(
    IMapper mapper,
    IPersonelCustomService personelCustomService,
    IPersonellerService personellerService,
    IPersonelCocuklariService personelCocuklariService,
    IDepartmanlarService departmanlarService,
    IServislerService servislerService,
    IUnvanlarService unvanlarService,
    IAtanmaNedenleriService atanmaNedenleriService,
    IHizmetBinalariService hizmetBinalariService,
    IIllerService illerService,
    IIlcelerService ilcelerService,
    ISendikalarService sendikalarService,
    IToastNotification toast) // 14 dependency!
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU - Facade pattern veya Mediator pattern
public PersonelController(
    IPersonelFacadeService personelFacade,
    IMapper mapper,
    IToastNotification toast) // Sadece 3 dependency
{
    // PersonelFacadeService içinde diğer service'ler encapsulate edilecek
}
```

---

### ✅ Düzeltme 6: DTO'larda Validation Attribute'larını Ekle
**Süre**: 1-2 hafta  
**Zorluk**: 🟢 Kolay  
**Etki**: 🔴 Kritik

#### Etkilenen Dosyalar:
```
✅ PersonelRequestDto.cs
✅ BankolarRequestDto.cs
✅ SiralarDto.cs
✅ LoginDto.cs
✅ KanalIslemleriDto.cs
✅ 45+ diğer DTO dosyaları
```

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ - Validation yok
public class PersonelRequestDto
{
    public string TcKimlikNo { get; set; } // Validation eksik!
    public string AdSoyad { get; set; }    // Validation eksik!
    public int SicilNo { get; set; }       // Range validation eksik!
}
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU - Validation attribute'ları
public class PersonelRequestDto
{
    [Required(ErrorMessage = "TC Kimlik No zorunludur")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır")]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "TC Kimlik No sadece rakam içermelidir")]
    public string TcKimlikNo { get; set; }
    
    [Required(ErrorMessage = "Ad Soyad zorunludur")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır")]
    public string AdSoyad { get; set; }
    
    [Range(1, 999999, ErrorMessage = "Sicil No 1-999999 arasında olmalıdır")]
    public int SicilNo { get; set; }
}
```

---

### ✅ Düzeltme 7: ViewComponent'lerde Fake Async Anti-Pattern'ini Düzelt
**Süre**: 1 gün  
**Zorluk**: 🟢 Kolay  
**Etki**: 🟡 Orta

#### Etkilenen Dosyalar:
```
✅ NavBarViewComponent.cs
✅ FooterViewComponent.cs
✅ LogoViewComponent.cs
✅ SearchViewComponent.cs
✅ UserActionViewComponent.cs
✅ LayoutMenuViewComponent.cs
```

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ - Fake async (Anti-pattern)
public async Task<IViewComponentResult> InvokeAsync()
{
    return await Task.FromResult(View()); // Gereksiz async!
}
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU - Synchronous method
public IViewComponentResult Invoke()
{
    return View(); // Basit ve performanslı
}
```

## 🟡 ÖNCELIK 2: ORTA SEVİYE DÜZELTMELER (1. Öncelik tamamlandıktan sonra)

### ✅ Düzeltme 8: Dependency Injection'ı Güncelle
**Süre**: 3-4 gün  
**Zorluk**: 🟢 Kolay  
**Etki**: 🟡 Orta

#### Program.cs'te Eklenecekler:
```csharp
// ✅ Repository'leri DI'a ekle
services.AddScoped<IKanallarCustomRepository, KanallarCustomRepository>();
services.AddScoped<IBankolarCustomRepository, BankolarCustomRepository>();
services.AddScoped<IPersonelCustomRepository, PersonelCustomRepository>();
services.AddScoped<ISiralarCustomRepository, SiralarCustomRepository>();
// ... diğer repository'ler

// ✅ AutoMapper konfigürasyonu (mevcut, değişmeyecek)
services.AddAutoMapper(typeof(AutoMapperProfile));
```

---

### ✅ Düzeltme 9: Magic Number'ları Constants'a Çevir
**Süre**: 2-3 gün  
**Zorluk**: 🟢 Kolay  
**Etki**: 🟡 Orta

#### Oluşturulacak Constants:
```csharp
// ✅ Constants sınıfı oluştur
public static class ApplicationConstants
{
    public static class SiraDurumu
    {
        public const int Bekliyor = 1;
        public const int Cagrildi = 2;
        public const int Isleniyor = 3;
        public const int Tamamlandi = 4;
        public const int Iptal = 5;
    }
    
    public static class DefaultValues
    {
        public const string DefaultPersonelResmi = "empty.png";
        public const int DefaultPageSize = 50;
        public const int MaxSiraSayisi = 100;
    }
}
```

---

### ✅ Düzeltme 10: AsNoTracking Ekle (Performans)
**Süre**: 1-2 gün  
**Zorluk**: 🟢 Kolay  
**Etki**: 🟡 Orta

#### Düzeltilecek Yerler:
```csharp
// ✅ Read-only operasyonlarda AsNoTracking ekle
public async Task<List<TEntity>> GetAllAsync()
{
    return await _context.Set<TEntity>()
        .AsNoTracking() // Ekle
        .ToListAsync();
}
```

---

## 🟢 ÖNCELIK 3: DÜŞÜK SEVİYE DÜZELTMELER (Son aşama)

### ✅ Düzeltme 11: N+1 Query Problem'ini Çöz
**Süre**: 1 hafta  
**Zorluk**: 🟡 Orta  
**Etki**: 🔴 Kritik (Performans)

#### Etkilenen Dosyalar:
```
✅ KanallarCustomService.cs
✅ BankolarCustomService.cs
✅ PersonelCustomService.cs
✅ SiralarCustomService.cs
```

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ - N+1 Query
foreach(var kanal in kanallar)
{
    var detay = await _context.KanalDetay
        .FirstOrDefaultAsync(x => x.KanalId == kanal.Id); // Her iterasyonda DB'ye gidiyor!
}
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU - Eager Loading
var kanallar = await _context.Kanallar
    .Include(k => k.KanalDetay) // Tek sorguda tüm data
    .AsNoTracking()
    .ToListAsync();
```

## 🟢 ÖNCELIK 3: DÜŞÜK SEVİYE DÜZELTMELER (Son aşama)

### ✅ Düzeltme 12: Anemic Domain Model'i Zenginleştir
**Süre**: 3-4 hafta  
**Zorluk**: 🔴 Zor  
**Etki**: 🟡 Orta

#### Etkilenen Dosyalar:
```
✅ Tüm DTO sınıfları (50+ dosya)
✅ Entity sınıfları (25+ dosya)
```

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ - Anemic Model (Sadece property'ler)
public class BankolarDto
{
    public int BankoId { get; set; }
    public int BankoNo { get; set; }
    public BankoTipi BankoTipi { get; set; }
    // Hiç method yok!
}
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU - Rich Domain Model
public class BankolarDto
{
    public int BankoId { get; set; }
    public int BankoNo { get; set; }
    public BankoTipi BankoTipi { get; set; }
    
    // Business logic methods
    public bool IsActive() => BankoAktiflik == Aktiflik.Aktif;
    public bool CanAssignPersonel() => IsActive() && BankoTipi != BankoTipi.Maintenance;
    public string GetDisplayName() => $"Banko {BankoNo} ({BankoTipi.GetDisplayName()})";
    public void Activate() => BankoAktiflik = Aktiflik.Aktif;
    public void Deactivate() => BankoAktiflik = Aktiflik.Pasif;
}
```

### ✅ Düzeltme 13: Exception Handling Ekle
**Süre**: 1 hafta  
**Zorluk**: 🟡 Orta  
**Etki**: 🟢 Düşük

### ✅ Düzeltme 14: Logging'i İyileştir
**Süre**: 3-4 gün  
**Zorluk**: 🟡 Orta  
**Etki**: 🟢 Düşük

### ✅ Düzeltme 15: Entity Constructor'larda Business Logic Kaldır
**Süre**: 2-3 gün  
**Zorluk**: 🟢 Kolay  
**Etki**: 🟢 Düşük

#### Etkilenen Dosyalar:
```
✅ Siralar.cs
✅ Bankolar.cs
✅ Personeller.cs
```

#### Mevcut Yanlış Kod:
```csharp
// ❌ YANLIŞ - Constructor'da business logic
public Siralar()
{
    _siraAlisTarihi = DateTime.Now.Date; // Side effect!
    BeklemeDurum = BeklemeDurum.Bekliyor; // Business logic!
}
```

#### Düzeltilmiş Kod:
```csharp
// ✅ DOĞRU - Factory pattern veya Builder pattern
public class SiralarFactory
{
    public static Siralar CreateNew(int kanalAltIslemId, int hizmetBinasiId)
    {
        return new Siralar
        {
            KanalAltIslemId = kanalAltIslemId,
            HizmetBinasiId = hizmetBinasiId,
            SiraAlisZamani = DateTime.Now,
            BeklemeDurum = BeklemeDurum.Bekliyor
        };
    }
}
```

---

## 📅 Haftalık Düzeltme Planı

### Hafta 1: Düzeltme 1-2 (Güvenlik + Fake Async)
- [ ] LoginController.cs - Cookie'lerde sensitive data kaldır
- [ ] NavBarViewComponent.cs - Fake async düzelt
- [ ] FooterViewComponent.cs - Fake async düzelt
- [ ] LogoViewComponent.cs - Fake async düzelt
- [ ] SearchViewComponent.cs - Fake async düzelt
- [ ] UserActionViewComponent.cs - Fake async düzelt
- [ ] LayoutMenuViewComponent.cs - Fake async düzelt

### Hafta 2-3: Düzeltme 3 (Context Dependency)
- [ ] KanallarCustomService.cs - Context dependency kaldır
- [ ] BankolarCustomService.cs - Context dependency kaldır
- [ ] PersonelCustomService.cs - Context dependency kaldır
- [ ] SiralarCustomService.cs - Context dependency kaldır
- [ ] Diğer 10 Custom Service - Context dependency kaldır

### Hafta 4-5: Düzeltme 4 (Repository)
- [ ] IKanallarCustomRepository oluştur
- [ ] KanallarCustomRepository implement et
- [ ] IBankolarCustomRepository oluştur
- [ ] BankolarCustomRepository implement et
- [ ] Diğer repository'leri oluştur

### Hafta 6: Düzeltme 5 (GenericRepository)
- [ ] GenericRepository'yi düzelt
- [ ] Interface'leri güncelle
- [ ] Service'lerde AutoMapper kullanımını düzelt

### Hafta 7-8: Düzeltme 6-7 (Controller God Object)
- [ ] PersonelController'ı böl (Facade pattern)
- [ ] ApiController'ı modülerize et
- [ ] TvController'ı düzelt
- [ ] YetkilerController'ı düzelt

### Hafta 9-10: Düzeltme 8 (DTO Validation)
- [ ] PersonelRequestDto.cs - Validation ekle
- [ ] BankolarRequestDto.cs - Validation ekle
- [ ] SiralarDto.cs - Validation ekle
- [ ] LoginDto.cs - Validation ekle
- [ ] 45+ diğer DTO - Validation ekle

### Hafta 11-12: Düzeltme 9-11 (Performans)
- [ ] DI konfigürasyonunu güncelle
- [ ] Magic number'ları düzelt
- [ ] AsNoTracking ekle
- [ ] N+1 Query'leri düzelt

### Hafta 13-16: Düzeltme 12-15 (Domain Model)
- [ ] Anemic Domain Model'i zenginleştir
- [ ] Exception handling ekle
- [ ] Logging'i iyileştir
- [ ] Entity constructor'ları düzelt

---

## 🎯 Her Düzeltme İçin Kontrol Listesi

### ✅ Düzeltme Tamamlandığında Kontrol Et:
- [ ] Kod compile oluyor mu?
- [ ] Unit test'ler geçiyor mu?
- [ ] AutoMapper konfigürasyonu çalışıyor mu?
- [ ] Dependency Injection çalışıyor mu?
- [ ] Performance regresyon var mı?
- [ ] Existing functionality bozulmadı mı?

---

## 📊 İlerleme Takibi

### Tamamlanan Düzeltmeler:
- [ ] Düzeltme 1: Cookie'lerde Sensitive Data Güvenlik Açığını Düzelt
- [ ] Düzeltme 2: Custom Service'lerden Context Kaldırma
- [ ] Düzeltme 3: Custom Repository'ler Oluşturma
- [ ] Düzeltme 4: GenericRepository Düzeltme
- [ ] Düzeltme 5: Controller'lardaki God Object Anti-Pattern'ini Düzelt
- [ ] Düzeltme 6: DTO'larda Validation Attribute'larını Ekle
- [ ] Düzeltme 7: ViewComponent'lerde Fake Async Anti-Pattern'ini Düzelt
- [ ] Düzeltme 8: Dependency Injection Güncelleme
- [ ] Düzeltme 9: Magic Number'ları Constants'a Çevirme
- [ ] Düzeltme 10: AsNoTracking Ekleme
- [ ] Düzeltme 11: N+1 Query Problem'ini Çöz
- [ ] Düzeltme 12: Anemic Domain Model'i Zenginleştir
- [ ] Düzeltme 13: Exception Handling Ekleme
- [ ] Düzeltme 14: Logging İyileştirme
- [ ] Düzeltme 15: Entity Constructor'larda Business Logic Kaldır

### Genel İlerleme: 0/15 (%0)

Bu liste, **AutoMapper kullanımını koruyarak** ve **sıralı şekilde** düzeltmeleri yapmanızı sağlayacaktır. Her düzeltme bir öncekine bağlı olduğu için sırayla gitmeniz önemli!
