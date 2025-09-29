# Presentation Layer (Sunum Katmanı) Şeması

## 📁 Klasör Yapısı

```
SocialSecurityInstitution.PresentationLayer/
├── 📁 Components/ (7 dosya)
├── 📁 Controllers/ (16 dosya)
│   ├── 📄 ApiController.cs
│   ├── 📄 AtanmaNedenleriController.cs
│   ├── 📄 BankoController.cs
│   ├── 📄 DepartmanController.cs
│   ├── 📄 HizmetBinalariController.cs
│   ├── 📄 HomeController.cs
│   ├── 📄 KanalController.cs (En büyük - 767 satır)
│   ├── 📄 KioskController.cs
│   ├── 📄 LoginController.cs
│   ├── 📄 LogoutController.cs
│   ├── 📄 PersonelController.cs
│   ├── 📄 ServisController.cs
│   ├── 📄 SiralarController.cs
│   ├── 📄 TvController.cs
│   ├── 📄 UnvanController.cs
│   └── 📄 YetkilerController.cs
├── 📁 Middleware/ (4 dosya)
├── 📁 Models/ (9 dosya)
├── 📁 Services/ (2 dosya)
├── 📁 Views/ (54 dosya)
├── 📁 wwwroot/ (1956 dosya)
├── 📁 Properties/
├── 📄 Program.cs (16KB - Ana başlangıç dosyası)
├── 📄 appsettings.json
├── 📄 appsettings.Development.json
├── 📄 libman.json
└── 📄 Dockerfile
```

## 🎮 Controller'lar ve İşlevleri

### 🏠 HomeController
- **Boyut**: 943 bytes (35 satır)
- **İşlevler**: 
  - Index() - Ana sayfa
  - Privacy() - Gizlilik sayfası
  - Error() - Hata yönetimi
- **Özellikler**: [Authorize] attribute ile korumalı

### 🔗 KanalController (En Kapsamlı)
- **Boyut**: 30,936 bytes (767 satır)
- **İşlevler**: 
  - Kanal CRUD işlemleri
  - Alt kanal yönetimi
  - Kanal işlemleri yönetimi
  - Personel-kanal eşleştirme
  - Drag & Drop demo sayfaları
- **Dependency Injection**: 13 farklı servis
- **Özellikler**: 
  - AutoMapper kullanımı
  - Toast bildirimleri
  - Async/await pattern
  - JSON API endpoint'leri

### 🏦 BankoController
- **Boyut**: 13,892 bytes
- **İşlevler**: Banko (sıra) yönetim sistemi

### 👥 PersonelController
- **Boyut**: 6,432 bytes
- **İşlevler**: Personel bilgileri yönetimi

### 🏢 DepartmanController
- **Boyut**: 5,337 bytes
- **İşlevler**: Departman organizasyonu

### 🔐 LoginController
- **Boyut**: 6,691 bytes
- **İşlevler**: Kimlik doğrulama

### 📺 TvController
- **Boyut**: 12,621 bytes
- **İşlevler**: TV ekranları yönetimi

### 🖥️ KioskController
- **Boyut**: 19,537 bytes
- **İşlevler**: Kiosk sistemleri

## 🔧 Teknik Özellikler

### Kullanılan Paketler
- **AutoMapper**: Nesne eşleme
- **NToastNotify**: Bildirim sistemi
- **SignalR**: Gerçek zamanlı iletişim
- **Entity Framework Core**: ORM

### Middleware Yapısı
- 4 farklı middleware bileşeni
- Özel hata yönetimi
- Kimlik doğrulama middleware

### Model Yapısı
- 9 farklı model sınıfı
- ViewModel'lar
- Request/Response modelleri

### View Yapısı
- 54 farklı view dosyası
- Razor Pages kullanımı
- Responsive tasarım

### Static Dosyalar (wwwroot)
- **1956 dosya** içeriyor
- CSS, JavaScript, resim dosyaları
- Third-party kütüphaneler
- Font dosyaları

## 🎯 Ana İşlevsel Akışlar

### 1. Kanal Yönetimi Akışı
```
KanallarListele → KanallarGetir → KanallarGuncelle/Ekle/Sil
                ↓
KanalAltIslemleri → PersonelEslestirme → DragDropDemo
```

### 2. Kimlik Doğrulama Akışı
```
Login → Authorization Check → Controller Actions → Logout
```

### 3. Banko İşlemleri Akışı
```
Banko Listesi → Sıra Yönetimi → Personel Atama → TV/Kiosk Gösterimi
```

## 🔐 Güvenlik Katmanları

1. **Controller Level**: [Authorize] attribute
2. **Action Level**: Özel yetkilendirme
3. **Middleware Level**: Kimlik doğrulama
4. **View Level**: Conditional rendering

## 📊 Performans Özellikleri

- **Async/Await**: Tüm controller'larda
- **Dependency Injection**: Temiz kod yapısı
- **AutoMapper**: Hızlı nesne dönüşümü
- **SignalR**: Gerçek zamanlı güncellemeler
