# Sosyal Güvenlik Kurumu Projesi - Genel Yapı

## 📁 Proje Hiyerarşisi

```
SocialSecurityInstitution/
├── 📄 SocialSecurityInstitution.sln
├── 📄 Dockerfile
├── 📄 docker-compose.yml
├── 📄 .gitignore
├── 📄 .gitattributes
├── 📄 .dockerignore
├── 📁 .git/
├── 📁 .github/
├── 📁 .vs/
├── 📁 CommonLayer/
├── 📁 SocialSecurityInstitution.PresentationLayer/
├── 📁 SocialSecurityInstitution.BusinessLogicLayer/
├── 📁 SocialSecurityInstitution.DataAccessLayer/
└── 📁 SocialSecurityInstitution.BusinessObjectLayer/
```

## 🏗️ Mimari Yaklaşımı

Bu proje **4-Katmanlı Mimari (N-Tier Architecture)** kullanmaktadır:

### 1. Presentation Layer (Sunum Katmanı)
- **Teknoloji**: ASP.NET Core MVC
- **Sorumluluk**: Kullanıcı arayüzü ve HTTP isteklerini yönetme
- **Dosya Sayısı**: 2000+ dosya

### 2. Business Logic Layer (İş Mantığı Katmanı)
- **Teknoloji**: .NET Core Class Library
- **Sorumluluk**: İş kuralları ve mantık işlemleri
- **Dosya Sayısı**: 115 dosya

### 3. Data Access Layer (Veri Erişim Katmanı)
- **Teknoloji**: Entity Framework Core
- **Sorumluluk**: Veritabanı işlemleri ve veri erişimi
- **Dosya Sayısı**: 89 dosya

### 4. Business Object Layer (İş Nesneleri Katmanı)
- **Teknoloji**: .NET Core Class Library
- **Sorumluluk**: Veri transfer nesneleri (DTO) ve entity'ler
- **Dosya Sayısı**: 100 dosya

## 🔧 Kullanılan Teknolojiler

- **.NET Core**: Ana framework
- **ASP.NET Core MVC**: Web uygulaması
- **Entity Framework Core**: ORM
- **AutoMapper**: Nesne eşleme
- **SignalR**: Gerçek zamanlı iletişim
- **NToastNotify**: Bildirim sistemi
- **Docker**: Konteynerizasyon
- **Git**: Versiyon kontrolü

## 📊 Proje İstatistikleri

| Katman | Dosya Sayısı | Ana Sorumluluk |
|--------|--------------|----------------|
| PresentationLayer | 2055 | UI/UX, Controllers, Views |
| BusinessLogicLayer | 115 | İş mantığı, servisler |
| DataAccessLayer | 89 | Veri erişimi, repository |
| BusinessObjectLayer | 100 | DTO'lar, entity'ler |
| **TOPLAM** | **2359** | **Tam SGK Yönetim Sistemi** |

## 🎯 Ana İşlevsel Modüller

1. **Kanal Yönetimi** - En kapsamlı modül
2. **Personel Yönetimi** - Çalışan bilgileri
3. **Banko İşlemleri** - Sıra yönetimi
4. **Departman Yönetimi** - Organizasyon
5. **Hizmet Binaları** - Lokasyon yönetimi
6. **TV/Kiosk Sistemleri** - Dijital bilgilendirme
7. **Kimlik Doğrulama** - Login/Logout

## 🔐 Güvenlik Özellikleri

- **Authorization**: Tüm controller'larda yetkilendirme
- **Authentication**: Kimlik doğrulama sistemi
- **Role-based Access**: Rol tabanlı erişim kontrolü
