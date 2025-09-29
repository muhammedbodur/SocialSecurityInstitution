# 📅 GÜNLÜK ÇALIŞMA PLANI - MİMARİ REFACTORİNG

## 🎯 **GENEL STRATEJİ**
**Toplam Süre**: 18-22 hafta (90-110 iş günü)  
**Başlangıç**: 5 Ağustos 2024 (Pazartesi)  
**Hedef Bitiş**: 23 Aralık 2024 (Pazartesi)  

**Öncelik Sırası**: Güvenlik → Kritik Mimari → Performans → Code Quality

---

## 📊 **HAFTALIK FAZLAR ÖZET**

| Hafta | Faz | Odak | Tahmini Süre |
|-------|-----|------|--------------|
| 1-2 | **Acil Güvenlik** | Cookie, Validation, Fake Async | 10 gün |
| 3-6 | **Kritik Mimari** | Service Context, Repository Pattern | 20 gün |
| 7-10 | **Controller Refactoring** | God Object, Dependency Injection | 20 gün |
| 11-14 | **Domain Model** | Anemic → Rich Domain, Business Logic | 20 gün |
| 15-18 | **Performance & Quality** | N+1 Query, Code Cleanup | 20 gün |
| 19-22 | **Testing & Documentation** | Unit Tests, Integration Tests | 20 gün |

---

## 🗓️ **DETAYLI GÜNLÜK PLAN**

---

## 📅 **HAFTA 1: ACİL GÜVENLİK DÜZELTMELERİ**

### **🔴 PAZARTESİ - 5 AĞUSTOS 2024**
**Tema**: Cookie Güvenlik Açığı Düzeltme

#### **Sabah (09:00-12:00)**
- [ ] **LoginController.cs** - Cookie'lerden TC Kimlik No kaldırma
- [ ] **Secure Cookie** implementasyonu (HttpOnly, Secure flags)
- [ ] **JWT Token** yapısına geçiş planlaması

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **AuthenticationMiddleware.cs** - Güvenli authentication flow
- [ ] **Session Management** - Secure session handling
- [ ] **Test**: Login/Logout güvenlik testleri

**Çıktı**: Cookie güvenlik açığı %100 çözülmüş olacak

---

### **🔴 SALI - 6 AĞUSTOS 2024**
**Tema**: DTO Validation Attribute - Kritik DTO'lar

#### **Sabah (09:00-12:00)**
- [ ] **PersonelRequestDto.cs** - Comprehensive validation attributes
- [ ] **LoginDto.cs** - Authentication validation
- [ ] **BankolarRequestDto.cs** - Business validation

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **Custom Validation Attributes** oluşturma:
  - [ ] `TcKimlikNoValidationAttribute`
  - [ ] `AgeValidationAttribute`
  - [ ] `NoHtmlContentAttribute` (XSS koruması)
- [ ] **Test**: Validation attribute testleri

**Çıktı**: En kritik 3 DTO'da validation %100 tamamlanmış

---

### **🟡 ÇARŞAMBA - 7 AĞUSTOS 2024**
**Tema**: ViewComponent Fake Async Düzeltme

#### **Sabah (09:00-12:00)**
- [ ] **6 ViewComponent** dosyasında fake async tespit etme
- [ ] **Synchronous** metotlara çevirme
- [ ] **Performance** ölçümü (before/after)

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **ViewComponent** business logic'i service'lere taşıma
- [ ] **Dependency Injection** düzeltmeleri
- [ ] **Test**: ViewComponent render testleri

**Çıktı**: ViewComponent performance %30-50 artış

---

### **🟡 PERŞEMBE - 8 AĞUSTOS 2024**
**Tema**: Magic Number ve Constants

#### **Sabah (09:00-12:00)**
- [ ] **Enum'lardaki magic number'lar** tespit etme
- [ ] **Constants sınıfları** oluşturma
- [ ] **BeklemeDurum, BankoTipi, KatTipi** enum'larını düzeltme

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **Hardcoded string'ler** tespit etme
- [ ] **Resource files** oluşturma
- [ ] **Configuration constants** düzenleme

**Çıktı**: Magic number'lar %80 azaltılmış

---

### **🟢 CUMA - 9 AĞUSTOS 2024**
**Tema**: Hafta Sonu Değerlendirme ve Planlama

#### **Sabah (09:00-12:00)**
- [ ] **Haftalık progress review**
- [ ] **Security scan** çalıştırma
- [ ] **Performance benchmark** alma

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **Gelecek hafta planlaması**
- [ ] **Code review** ve **documentation**
- [ ] **Git commit** ve **branch** organizasyonu

**Çıktı**: Hafta 1 tamamlanmış, güvenlik %70 artırılmış

---

## 📅 **HAFTA 2: VALİDATİON VE GÜVENLİK TAMAMLAMA**

### **🔴 PAZARTESİ - 12 AĞUSTOS 2024**
**Tema**: Kalan DTO Validation'ları

#### **Sabah (09:00-12:00)**
- [ ] **KanallarDto, DepartmanDto, HizmetBinalariDto** validation
- [ ] **SiralarDto, TvEkranlariDto** validation
- [ ] **Batch validation** script'i yazma

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **15+ DTO** daha validation attribute ekleme
- [ ] **Validation error handling** middleware
- [ ] **Client-side validation** JavaScript

**Çıktı**: 25+ DTO validation tamamlanmış

---

### **🔴 SALI - 13 AĞUSTOS 2024**
**Tema**: Global Exception Handling

#### **Sabah (09:00-12:00)**
- [ ] **GlobalExceptionMiddleware** oluşturma
- [ ] **Custom exception types** tanımlama
- [ ] **Logging integration** (Serilog)

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **Error response standardization**
- [ ] **Security exception handling**
- [ ] **User-friendly error messages**

**Çıktı**: Exception handling %100 merkezi hale getirilmiş

---

## 📅 **HAFTA 3-6: KRİTİK MİMARİ - SERVİS KATMANI**

### **🔴 PAZARTESİ - 19 AĞUSTOS 2024**
**Tema**: Service Layer Context Dependency Analizi

#### **Sabah (09:00-12:00)**
- [ ] **16 Custom Service** dosyasında Context usage analizi
- [ ] **Dependency mapping** (hangi service hangi repository'ye ihtiyaç duyuyor)
- [ ] **Migration strategy** belirleme

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **IKanallarCustomService** interface düzeltme
- [ ] **Repository method** signatures planlama
- [ ] **AutoMapper configuration** hazırlığı

**Çıktı**: Service refactoring stratejisi netleşmiş

---

### **🔴 SALI - 20 AĞUSTOS 2024**
**Tema**: KanallarCustomService Refactoring

#### **Sabah (09:00-12:00)**
- [ ] **KanallarCustomService.cs** - Context dependency kaldırma
- [ ] **IKanallarDal** interface'ine yeni metotlar ekleme
- [ ] **Repository implementation** güncelleme

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **AutoMapper configuration** düzeltme
- [ ] **Unit test** yazma
- [ ] **Integration test** güncelleme

**Çıktı**: KanallarCustomService %100 refactor edilmiş

---

## 📅 **HAFTA 7-10: CONTROLLER GOD OBJECT DÜZELTMESİ**

### **🔴 PAZARTESİ - 2 EYLÜL 2024**
**Tema**: PersonelController God Object Analizi

#### **Sabah (09:00-12:00)**
- [ ] **PersonelController** - 14 dependency analizi
- [ ] **Responsibility segregation** planlama
- [ ] **Facade pattern** tasarımı

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **IPersonelFacade** interface tasarlama
- [ ] **PersonelFacade** implementation başlangıcı
- [ ] **Dependency reduction** stratejisi

**Çıktı**: PersonelController refactoring planı hazır

---

## 📅 **HAFTA 11-14: DOMAİN MODEL ZENGİNLEŞTİRME**

### **🔴 PAZARTESİ - 9 EYLÜL 2024**
**Tema**: Anemic Domain Model Analizi

#### **Sabah (09:00-12:00)**
- [ ] **75+ sınıf** anemic model analizi
- [ ] **Business logic** dağılımı mapping
- [ ] **Rich domain model** tasarımı

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **Domain service** vs **Entity method** kararları
- [ ] **Value object** identification
- [ ] **Aggregate root** tasarımı

**Çıktı**: Rich domain model stratejisi hazır

---

## 📅 **HAFTA 15-18: PERFORMANS OPTİMİZASYONU**

### **🔴 PAZARTESİ - 16 EYLÜL 2024**
**Tema**: N+1 Query Problem Analizi

#### **Sabah (09:00-12:00)**
- [ ] **N+1 query** detection tool çalıştırma
- [ ] **Problematic queries** identification
- [ ] **Include strategy** planlama

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **Eager loading** vs **Lazy loading** kararları
- [ ] **Projection queries** planlama
- [ ] **Query optimization** stratejisi

**Çıktı**: Performance optimization planı hazır

---

## 📅 **HAFTA 19-22: TESLİM VE DOKÜMANTASYON**

### **🟢 PAZARTESİ - 30 EYLÜL 2024**
**Tema**: Comprehensive Testing

#### **Sabah (09:00-12:00)**
- [ ] **Unit test** coverage analysis
- [ ] **Integration test** execution
- [ ] **End-to-end test** scenarios

#### **Öğleden Sonra (13:00-17:00)**
- [ ] **Performance test** execution
- [ ] **Load testing**
- [ ] **Security testing**

**Çıktı**: Test coverage %80+ achieved

---

## 📊 **GÜNLÜK TAKİP ÇİZELGESİ**

### **Her Gün Yapılacaklar**:
- [ ] **Daily standup** (09:00) - Progress review
- [ ] **Git commit** (17:00) - Daily progress commit
- [ ] **Progress tracking** - Günlük ilerleme kaydı
- [ ] **Issue logging** - Karşılaşılan sorunları kaydetme

### **Haftalık Yapılacaklar**:
- [ ] **Weekly review** (Cuma 16:00) - Haftalık değerlendirme
- [ ] **Next week planning** (Cuma 17:00) - Gelecek hafta planlaması
- [ ] **Stakeholder update** - İlerleme raporu
- [ ] **Risk assessment** - Risk değerlendirmesi

---

## 🎯 **BAŞARI KRİTERLERİ**

### **Hafta 1-2 Sonunda**:
- ✅ Cookie güvenlik açığı %100 çözülmüş
- ✅ 45+ DTO validation attribute eklenmiş
- ✅ ViewComponent fake async düzeltilmiş
- ✅ Magic number'lar %80 azaltılmış

### **Hafta 6 Sonunda**:
- ✅ Service layer context dependency %100 kaldırılmış
- ✅ Repository pattern düzeltilmiş
- ✅ SOLID compliance %50'ye çıkarılmış

### **Hafta 10 Sonunda**:
- ✅ Controller God Object sorunu çözülmüş
- ✅ Dependency injection optimize edilmiş
- ✅ Presentation layer refactor tamamlanmış

### **Hafta 14 Sonunda**:
- ✅ Anemic domain model zenginleştirilmiş
- ✅ Business logic entity'lere taşınmış
- ✅ Value object'ler implementasyonu tamamlanmış

### **Hafta 18 Sonunda**:
- ✅ N+1 query problem %90 çözülmüş
- ✅ Performance %3x artırılmış
- ✅ Code quality %85+ seviyesinde

### **Hafta 22 Sonunda**:
- ✅ Test coverage %80+ achieved
- ✅ Documentation %95 tamamlanmış
- ✅ Production ready codebase

---

## 🚨 **ACİL BAŞLANACAK GÖREVLER (YARIN)**

### **6 AĞUSTOS 2024 - SALI**
1. **🔴 LoginController** - Cookie'lerden TC Kimlik No kaldır
2. **🔴 PersonelRequestDto** - Validation attribute'ları ekle
3. **🔴 Custom Validation** - TcKimlikNoValidation oluştur

### **7 AĞUSTOS 2024 - ÇARŞAMBA**
1. **🟡 ViewComponent'ler** - Fake async tespit et ve düzelt
2. **🟡 Magic Number'lar** - Enum'lardaki değerleri constants'a çevir

**Bu plan ile 22 hafta sonunda projenizin mimari kalitesi %18'den %85'e çıkacak!** 🚀
