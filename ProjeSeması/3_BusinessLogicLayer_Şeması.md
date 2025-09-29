# Business Logic Layer (İş Mantığı Katmanı) Şeması

## 📁 Klasör Yapısı

```
SocialSecurityInstitution.BusinessLogicLayer/
├── 📁 AbstractLogicServices/ (36 dosya)
│   ├── 📄 IAtanmaNedenleriService.cs
│   ├── 📄 IBankolarService.cs
│   ├── 📄 IDepartmanlarService.cs
│   ├── 📄 IHizmetBinalariService.cs
│   ├── 📄 IKanallarService.cs
│   ├── 📄 IKanallarAltService.cs
│   ├── 📄 IKanalIslemleriService.cs
│   ├── 📄 IKanalAltIslemleriService.cs
│   ├── 📄 IKanalPersonelleriService.cs
│   ├── 📄 IPersonellerService.cs
│   ├── 📄 IServislerService.cs
│   ├── 📄 ISiralarService.cs
│   ├── 📄 IUnvanlarService.cs
│   ├── 📄 IYetkilerService.cs
│   └── ... (22 adet daha)
├── 📁 ConcreteLogicServices/ (35 dosya)
│   ├── 📄 AtanmaNedenleriService.cs
│   ├── 📄 BankolarService.cs
│   ├── 📄 DepartmanlarService.cs
│   ├── 📄 HizmetBinalariService.cs
│   ├── 📄 KanallarService.cs
│   ├── 📄 KanallarAltService.cs
│   ├── 📄 KanalIslemleriService.cs
│   ├── 📄 KanalAltIslemleriService.cs
│   ├── 📄 KanalPersonelleriService.cs
│   ├── 📄 PersonellerService.cs
│   ├── 📄 ServislerService.cs
│   ├── 📄 SiralarService.cs
│   ├── 📄 UnvanlarService.cs
│   ├── 📄 YetkilerService.cs
│   └── ... (21 adet daha)
├── 📁 CustomAbstractLogicService/ (17 dosya)
│   ├── 📄 IBankoCustomService.cs
│   ├── 📄 IKanallarCustomService.cs
│   ├── 📄 IKanalPersonelleriCustomService.cs
│   ├── 📄 IPersonelCustomService.cs
│   ├── 📄 IUserInfoService.cs
│   └── ... (12 adet daha)
├── 📁 CustomConcreteLogicService/ (19 dosya)
│   ├── 📄 BankoCustomService.cs
│   ├── 📄 KanallarCustomService.cs
│   ├── 📄 KanalPersonelleriCustomService.cs
│   ├── 📄 PersonelCustomService.cs
│   ├── 📄 UserInfoService.cs
│   └── ... (14 adet daha)
├── 📁 Helpers/ (1 dosya)
│   └── 📄 Helper.cs
├── 📁 Hubs/ (2 dosya)
│   ├── 📄 BankoHub.cs
│   └── 📄 TvHub.cs
├── 📁 MappingLogicService/ (2 dosya)
│   ├── 📄 AutoMapperProfile.cs
│   └── 📄 MappingService.cs
├── 📁 SqlDependencyServices/ (2 dosya)
│   ├── 📄 ISqlDependencyService.cs
│   └── 📄 SqlDependencyService.cs
└── 📄 SocialSecurityInstitution.BusinessLogicLayer.csproj
```

## 🏗️ Servis Mimarisi

### 1. Abstract Logic Services (Soyut Servisler)
**Amaç**: Interface tanımları ve sözleşmeler
- **36 farklı interface**
- **Generic CRUD operasyonları**
- **Async method signatures**

#### Ana Interface'ler:
```csharp
// Örnek Interface Yapısı
public interface IKanallarService
{
    Task<List<KanallarDto>> TGetAllAsync();
    Task<KanallarDto> TGetByIdAsync(int id);
    Task<bool> TAddAsync(KanallarDto entity);
    Task<bool> TUpdateAsync(KanallarDto entity);
    Task<bool> TDeleteAsync(int id);
}
```

### 2. Concrete Logic Services (Somut Servisler)
**Amaç**: Interface'lerin gerçek implementasyonları
- **35 farklı servis sınıfı**
- **Business logic implementation**
- **Data Access Layer ile iletişim**

#### Servis Kategorileri:
- **Kanal Servisleri**: Kanal yönetimi
- **Personel Servisleri**: Çalışan işlemleri
- **Banko Servisleri**: Sıra yönetimi
- **Departman Servisleri**: Organizasyon
- **Hizmet Binası Servisleri**: Lokasyon
- **Yetki Servisleri**: Rol yönetimi

### 3. Custom Logic Services (Özel Servisler)
**Amaç**: Karmaşık business logic ve özel işlemler

#### Custom Abstract (17 dosya):
- **IBankoCustomService**: Özel banko işlemleri
- **IKanallarCustomService**: Kanal eşleştirme
- **IPersonelCustomService**: Personel raporları
- **IUserInfoService**: Kullanıcı bilgileri

#### Custom Concrete (19 dosya):
- **Kompleks sorgular**
- **Multi-table operations**
- **Business rule validations**
- **Reporting services**

## 🔄 SignalR Hub'ları

### BankoHub.cs
```csharp
// Gerçek zamanlı banko güncellemeleri
public class BankoHub : Hub
{
    // Sıra durumu güncellemeleri
    // Personel bildirimleri
    // Müşteri çağrıları
}
```

### TvHub.cs
```csharp
// TV ekranları için gerçek zamanlı veri
public class TvHub : Hub
{
    // Sıra bilgileri
    // Duyurular
    // Sistem mesajları
}
```

## 🗺️ AutoMapper Konfigürasyonu

### AutoMapperProfile.cs
```csharp
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Entity to DTO mappings
        CreateMap<Kanallar, KanallarDto>();
        CreateMap<Personeller, PersonellerDto>();
        CreateMap<Bankolar, BankolarDto>();
        // ... diğer mapping'ler
    }
}
```

## 🔧 Helper Sınıfları

### Helper.cs
- **Utility methods**
- **Common operations**
- **Data validation**
- **Format conversions**

## 📊 SQL Dependency Services

### SqlDependencyService.cs
```csharp
// Veritabanı değişiklik bildirimleri
public class SqlDependencyService : ISqlDependencyService
{
    // Real-time database change notifications
    // Cache invalidation
    // Automatic UI updates
}
```

## 🎯 Servis Katmanı İşlevleri

### 1. CRUD Operations
```csharp
// Standart CRUD pattern
public async Task<bool> TAddAsync(EntityDto entity)
{
    // Validation
    // Business rules
    // Data access call
    // Result handling
}
```

### 2. Business Rules
- **Veri doğrulama**
- **İş kuralı kontrolü**
- **Yetkilendirme**
- **Audit logging**

### 3. Data Transformation
- **Entity ↔ DTO dönüşümü**
- **AutoMapper kullanımı**
- **Data formatting**
- **Validation**

## 🔐 Güvenlik ve Validasyon

### Business Rule Validations
- **Kanal eşleştirme kuralları**
- **Personel yetki kontrolü**
- **Departman hiyerarşi kuralları**
- **Banko operasyon kuralları**

### Data Integrity
- **Foreign key validations**
- **Business constraint checks**
- **Concurrent access handling**
- **Transaction management**

## 📈 Performans Optimizasyonları

### Async Pattern
- **Tüm servisler async/await**
- **Non-blocking operations**
- **Scalable architecture**

### Caching Strategy
- **SqlDependency ile cache invalidation**
- **Memory caching**
- **Distributed caching ready**

### Dependency Injection
- **Loose coupling**
- **Testable architecture**
- **IoC container integration**

## 🔄 Servis Bağımlılıkları

```
Controllers
    ↓
Abstract Logic Services
    ↓
Concrete Logic Services
    ↓
Custom Logic Services
    ↓
Data Access Layer
    ↓
Database
```

Bu katman, uygulamanın **kalbi** olarak işlev görür ve tüm iş mantığını merkezi olarak yönetir.
