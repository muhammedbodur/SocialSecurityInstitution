# 🚨 **KRİTİK HATALAR ODAK LİSTESİ**
## *Günlük Çalışma Planından Öncelikli Sorunlar*

---

## 🎯 **BUGÜN ODAKLANILACAK KRİTİK HATALAR**

### **1. 🔴 FAKE ASYNC VIEWCOMPONENT SORUNU** *(ÖNCELİK: YÜKSEK)*

**Dosya Konumları:**
```
SocialSecurityInstitution.PresentationLayer\ViewComponents\
├── BankoViewComponent.cs
├── KanalViewComponent.cs  
├── PersonelViewComponent.cs
├── DashboardViewComponent.cs
├── NotificationViewComponent.cs
└── StatisticsViewComponent.cs
```

**Sorun:**
- 6 ViewComponent'te fake async method'lar mevcut
- `Task.FromResult()` kullanımı performance kaybına neden oluyor
- Gereksiz async/await overhead'i

**Çözüm:**
```csharp
// ❌ YANLIŞ - Fake Async
public async Task<IViewComponentResult> InvokeAsync()
{
    var data = GetData();
    return await Task.FromResult(View(data));
}

// ✅ DOĞRU - Synchronous
public IViewComponentResult Invoke()
{
    var data = GetData();
    return View(data);
}
```

**Beklenen Kazanım:** %30-50 performance artışı

---

### **2. 🔴 SERVİS KATMANI CONTEXT DEPENDENCY** *(ÖNCELİK: YÜKSEK)*

**Dosya Konumları:**
```
SocialSecurityInstitution.BusinessLogicLayer\CustomConcreteLogicServices\
├── KanallarCustomService.cs ⚠️ Context dependency
├── PersonelCustomService.cs ⚠️ Context dependency  
├── BankolarCustomService.cs ⚠️ Context dependency
├── DepartmanlarCustomService.cs ⚠️ Context dependency
└── [12 dosya daha...]
```

**Sorun:**
- Service layer'da doğrudan DbContext kullanımı
- Repository pattern bypass edilmesi
- Tight coupling ve test edilemezlik

**Çözüm:**
```csharp
// ❌ YANLIŞ - Service'te Context
public class KanallarCustomService
{
    private readonly AppDbContext _context; // YANLIŞ!
    
    public List<Kanal> GetAll()
    {
        return _context.Kanallar.ToList(); // Anti-pattern!
    }
}

// ✅ DOĞRU - Repository Pattern
public class KanallarCustomService
{
    private readonly IKanallarDal _kanallarDal;
    
    public List<Kanal> GetAll()
    {
        return _kanallarDal.GetAll(); // Doğru yaklaşım
    }
}
```

---

### **3. 🔴 CONTROLLER GOD OBJECT SORUNU** *(ÖNCELİK: ORTA)*

**Dosya Konumları:**
```
SocialSecurityInstitution.PresentationLayer\Controllers\
├── PersonelController.cs ⚠️ 14 dependency
├── KanalController.cs ⚠️ 12 dependency
├── BankoController.cs ⚠️ 10 dependency
└── HomeController.cs ⚠️ 8 dependency
```

**Sorun:**
- Tek controller'da çok fazla sorumluluk
- Constructor'da 10+ dependency injection
- Single Responsibility Principle ihlali

**Çözüm:**
```csharp
// ❌ YANLIŞ - God Object
public class PersonelController : Controller
{
    private readonly IService1 _service1;
    private readonly IService2 _service2;
    // ... 14 dependency daha
    
    public PersonelController(IService1 s1, IService2 s2, ...) // Çok fazla!
}

// ✅ DOĞRU - Facade Pattern
public class PersonelController : Controller
{
    private readonly IPersonelFacade _personelFacade; // Tek dependency
    
    public PersonelController(IPersonelFacade facade)
    {
        _personelFacade = facade;
    }
}
```

---

### **4. 🟡 GLOBAL EXCEPTION HANDLING EKSİKLİĞİ** *(ÖNCELİK: ORTA)*

**Dosya Konumları:**
```
SocialSecurityInstitution.PresentationLayer\
├── Program.cs ⚠️ Exception middleware yok
├── Controllers\ ⚠️ Try-catch chaos
└── Middleware\ ⚠️ Custom exception handler yok
```

**Sorun:**
- Her controller'da ayrı try-catch blokları
- Inconsistent error handling
- Security bilgisi leak'i riski

**Çözüm:**
```csharp
// ✅ Global Exception Middleware
public class GlobalExceptionMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
}
```

---

### **5. 🟡 ANEMIC DOMAIN MODEL** *(ÖNCELİK: DÜŞÜK)*

**Dosya Konumları:**
```
SocialSecurityInstitution.BusinessObjectLayer\DatabaseEntities\
├── Personel.cs ⚠️ Sadece property'ler
├── Kanal.cs ⚠️ Business logic yok
├── Banko.cs ⚠️ Anemic model
└── [Tüm entity'ler...]
```

**Sorun:**
- Entity'lerde sadece property'ler var
- Business logic service'lerde dağınık
- Domain-driven design eksikliği

---

## 📋 **GÜNLÜK ÇALIŞMA ÖNCELİK SIRASI**

### **🌅 SABAH (09:00-12:00) - HIZLI KAZANIMLAR**
1. ✅ **ViewComponent Fake Async Düzeltme** (2-3 saat)
   - 6 dosyada async → sync conversion
   - Performance test before/after
   - **Beklenen Sonuç:** %30-50 performance artışı

### **🌆 ÖĞLEDEN SONRA (13:00-17:00) - DERİN REFACTORING**
2. 🔄 **Service Layer Context Dependency** (3-4 saat)
   - KanallarCustomService.cs başlangıç
   - Repository interface düzeltmeleri
   - **Beklenen Sonuç:** 1 service tamamen refactor

---

## 🎯 **HAFTALIK HEDEFLER**

| Gün | Odak | Dosya Sayısı | Beklenen Kazanım |
|-----|------|--------------|------------------|
| **Pazartesi** | ViewComponent + Service Layer | 8 dosya | Performance %40↑ |
| **Salı** | Service Layer devam | 4 dosya | Architecture %30↑ |
| **Çarşamba** | Controller Refactoring | 2 dosya | Maintainability %50↑ |
| **Perşembe** | Exception Handling | 5 dosya | Stability %60↑ |
| **Cuma** | Testing + Documentation | - | Quality %40↑ |

---

## 🚀 **HIZLI BAŞLANGIÇ KOMUTLARI**

### **ViewComponent Fake Async Tespiti:**
```bash
# Fake async pattern'leri bul
grep -r "Task.FromResult" SocialSecurityInstitution.PresentationLayer/ViewComponents/
grep -r "async.*InvokeAsync" SocialSecurityInstitution.PresentationLayer/ViewComponents/
```

### **Service Context Dependency Tespiti:**
```bash
# Context dependency'leri bul
grep -r "DbContext" SocialSecurityInstitution.BusinessLogicLayer/CustomConcreteLogicServices/
grep -r "_context\." SocialSecurityInstitution.BusinessLogicLayer/CustomConcreteLogicServices/
```

### **Controller God Object Tespiti:**
```bash
# Constructor parameter sayısını say
grep -A 20 "public.*Controller" SocialSecurityInstitution.PresentationLayer/Controllers/
```

---

## 📊 **BAŞARI METRİKLERİ**

| Kategori | Mevcut Durum | Hedef | Ölçüm Yöntemi |
|----------|--------------|-------|---------------|
| **Performance** | Baseline | %40↑ | Response time |
| **Maintainability** | 3/10 | 7/10 | Code complexity |
| **Testability** | 2/10 | 8/10 | Unit test coverage |
| **Architecture** | 4/10 | 8/10 | SOLID compliance |

---

## 🎯 **BUGÜNKÜ SOMUT HEDEFLER**

### ✅ **TAMAMLANACAKLAR:**
- [ ] 6 ViewComponent fake async → sync conversion
- [ ] KanallarCustomService.cs Context dependency kaldırma
- [ ] Performance benchmark before/after
- [ ] 1 unit test yazma

### 📈 **BEKLENEN SONUÇLAR:**
- **Performance:** %30-50 artış
- **Code Quality:** 2 kritik anti-pattern çözülmüş
- **Architecture:** Service layer 1 adım daha temiz
- **Maintainability:** ViewComponent'ler test edilebilir hale gelmiş

---

## 💡 **HIZLI İPUÇLARI**

1. **ViewComponent'lerde** `async` keyword'ünü kaldırmadan önce gerçekten async işlem var mı kontrol et
2. **Service refactoring'de** önce interface'i düzelt, sonra implementation'ı
3. **Test yazarken** önce failing test yaz, sonra fix et
4. **Performance ölçümünde** en az 3 kez ölç, ortalama al

---

**🚀 BAŞARILAR! Bu liste ile odaklanarak büyük ilerleme kaydedeceksiniz!**
