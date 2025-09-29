# 📋 Sosyal Güvenlik Kurumu Projesi - Şema Dokümantasyonu

Bu klasör, **Sosyal Güvenlik Kurumu (SGK) Yönetim Sistemi** projesinin kapsamlı şemasını içermektedir.

## 📁 Dokümantasyon İçeriği

### 1. [Proje Yapısı](./1_ProjeYapısı.md)
- Genel proje hiyerarşisi
- 4-katmanlı mimari açıklaması
- Kullanılan teknolojiler
- Proje istatistikleri
- Ana işlevsel modüller

### 2. [Presentation Layer Şeması](./2_PresentationLayer_Şeması.md)
- Controller'lar ve işlevleri
- View yapısı
- Model organizasyonu
- Middleware bileşenleri
- Static dosya yapısı
- SignalR Hub'ları

### 3. [Business Logic Layer Şeması](./3_BusinessLogicLayer_Şeması.md)
- Abstract ve Concrete servisler
- Custom logic servisler
- AutoMapper konfigürasyonu
- Helper sınıfları
- SQL Dependency servisleri
- Servis bağımlılıkları

### 4. [Data Access Layer Şeması](./4_DataAccessLayer_Şeması.md)
- Repository pattern implementasyonu
- Entity Framework konfigürasyonu
- Migration geçmişi
- Database context yapısı
- Generic repository pattern
- Performans optimizasyonları

### 5. [Business Object Layer Şeması](./5_BusinessObjectLayer_Şeması.md)
- DTO (Data Transfer Object) yapıları
- Database entity'leri
- Common entities ve enum'lar
- Extension methodları
- Validation attribute'ları
- AutoMapper profilleri

### 6. [Teknolojiler ve Bağımlılıklar](./6_Teknolojiler_ve_Bağımlılıklar.md)
- Kullanılan teknoloji stack'i
- NuGet paket bağımlılıkları
- Docker konfigürasyonu
- Development tools
- Deployment stratejileri
- Güvenlik konfigürasyonları

### 7. [İşlevsel Modüller ve Akışlar](./7_İşlevsel_Modüller_ve_Akışlar.md)
- Ana işlevsel modüller
- Kullanıcı senaryoları
- API endpoint'leri
- UI/UX akışları
- Gerçek zamanlı iletişim
- Performans metrikleri

### 8. [Veritabanı Şeması](./8_Veritabanı_Şeması.md)
- Tablo yapıları ve ilişkiler
- İndeks stratejileri
- Stored procedure'lar
- Güvenlik kısıtlamaları
- Backup ve maintenance
- Veri büyüklüğü tahminleri

## 🎯 Proje Özeti

### Genel Bilgiler
- **Proje Türü**: Enterprise Web Application
- **Teknoloji**: .NET Core, ASP.NET MVC, Entity Framework
- **Mimari**: 4-Katmanlı (N-Tier Architecture)
- **Veritabanı**: SQL Server
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap
- **Gerçek Zamanlı**: SignalR

### Ana Özellikler
- ✅ **Kanal Yönetimi**: Hizmet kanalları ve işlem tanımlama
- ✅ **Personel Yönetimi**: Çalışan bilgileri ve yetkilendirme
- ✅ **Banko Sistemi**: Sıra yönetimi ve müşteri hizmetleri
- ✅ **Departman Organizasyonu**: Hiyerarşik yapı yönetimi
- ✅ **Dijital Bilgilendirme**: TV ve Kiosk sistemleri
- ✅ **Gerçek Zamanlı Güncellemeler**: SignalR ile canlı veri
- ✅ **Güvenlik**: Authentication ve Authorization
- ✅ **Docker Desteği**: Konteynerizasyon

### Teknik Metrikler
- **Toplam Dosya**: 2359+ dosya
- **Kod Satırı**: 50,000+ satır (tahmini)
- **Controller**: 16 adet
- **Entity**: 34 adet
- **DTO**: 63 adet
- **Service**: 115+ adet
- **Migration**: 13 adet

### Performans Hedefleri
- **Eş Zamanlı Kullanıcı**: 500+ kullanıcı
- **Günlük İşlem**: 10,000+ işlem
- **Response Time**: <200ms ortalama
- **Uptime**: %99.9 hedef

## 🚀 Başlangıç Kılavuzu

### Gereksinimler
- .NET Core 6.0+
- SQL Server 2019+
- Visual Studio 2022
- Docker (opsiyonel)

### Kurulum Adımları
1. Repository'yi klonlayın
2. SQL Server bağlantı string'ini güncelleyin
3. Package'ları restore edin: `dotnet restore`
4. Veritabanını oluşturun: `dotnet ef database update`
5. Projeyi çalıştırın: `dotnet run`

### Docker ile Çalıştırma
```bash
docker-compose up -d
```

## 📞 İletişim ve Destek

Bu dokümantasyon, projenin teknik mimarisini ve işleyişini anlamak için hazırlanmıştır. 

**Not**: Bu şema dokümantasyonu, projenin mevcut durumunu yansıtmaktadır ve geliştirme sürecinde güncellenebilir.

---
*Son Güncelleme: 04.08.2025*
