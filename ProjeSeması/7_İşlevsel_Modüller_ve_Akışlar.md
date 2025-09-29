# İşlevsel Modüller ve Akışlar

## 🎯 Ana İşlevsel Modüller

### 1. 🔗 Kanal Yönetimi Modülü (En Kapsamlı)
**Controller**: `KanalController.cs` (767 satır)

#### Alt Modüller:
- **Kanal CRUD İşlemleri**
- **Alt Kanal Yönetimi**
- **Kanal İşlemleri**
- **Alt Kanal İşlemleri**
- **Personel-Kanal Eşleştirme**
- **Drag & Drop Demo**

#### İşlevsel Akış:
```
Kanal Listesi → Kanal Seçimi → İşlem Tanımlama → Alt İşlem Oluşturma → Personel Atama
     ↓              ↓              ↓                ↓                  ↓
Görüntüleme    Düzenleme      CRUD İşlemleri   Hiyerarşi        Yetkilendirme
```

#### Ana Methodlar:
- `KanallarListele()` - Tüm kanalları listele
- `KanallarEkle()` - Yeni kanal oluştur
- `KanallarGuncelle()` - Kanal bilgilerini güncelle
- `KanallarSil()` - Kanal silme işlemi
- `PersonelAltKanalEslestirmeYap()` - Personel atama
- `DragAndDropDemo()` - Sürükle-bırak demo

### 2. 🏦 Banko Yönetimi Modülü
**Controller**: `BankoController.cs` (13,892 bytes)

#### İşlevler:
- **Banko Tanımlama**
- **Sıra Yönetimi**
- **Personel Atama**
- **Çalışma Saatleri**
- **Kapasite Kontrolü**

#### İşlevsel Akış:
```
Banko Oluştur → Personel Ata → Çalışma Saati Belirle → Sıra Sistemi Aktif
      ↓             ↓              ↓                      ↓
   Lokasyon      Yetkilendirme   Zaman Planı          Müşteri Hizmeti
```

### 3. 👥 Personel Yönetimi Modülü
**Controller**: `PersonelController.cs` (6,432 bytes)

#### İşlevler:
- **Personel Kayıt**
- **Bilgi Güncelleme**
- **Departman Atama**
- **Unvan Belirleme**
- **Yetki Yönetimi**

#### İşlevsel Akış:
```
Personel Kaydı → Departman Atama → Unvan Belirleme → Yetki Verme → Kanal/Banko Atama
      ↓              ↓                ↓               ↓              ↓
   Kimlik         Organizasyon      Hiyerarşi      Güvenlik     İş Tanımı
```

### 4. 🏢 Departman Yönetimi Modülü
**Controller**: `DepartmanController.cs` (5,337 bytes)

#### İşlevler:
- **Departman Oluşturma**
- **Hiyerarşi Yönetimi**
- **Personel Organizasyonu**
- **Kanal Bağlantıları**

### 5. 🔐 Kimlik Doğrulama Modülü
**Controllers**: `LoginController.cs` (6,691 bytes), `LogoutController.cs` (3,006 bytes)

#### İşlevler:
- **Kullanıcı Girişi**
- **Oturum Yönetimi**
- **Yetki Kontrolü**
- **Güvenli Çıkış**

#### İşlevsel Akış:
```
Login Sayfası → Kimlik Doğrulama → Yetki Kontrolü → Ana Sayfa → Oturum Yönetimi
      ↓              ↓                ↓              ↓              ↓
  Credentials    Authentication    Authorization   Dashboard     Session
```

### 6. 📺 Dijital Bilgilendirme Modülleri
**Controllers**: `TvController.cs` (12,621 bytes), `KioskController.cs` (19,537 bytes)

#### TV Sistemi İşlevleri:
- **Sıra Durumu Gösterimi**
- **Duyuru Yayını**
- **Gerçek Zamanlı Güncellemeler**
- **Çoklu Ekran Yönetimi**

#### Kiosk Sistemi İşlevleri:
- **Self-Service İşlemler**
- **Sıra Alma**
- **Bilgi Sorgulama**
- **Yönlendirme**

### 7. 🏗️ Hizmet Binası Yönetimi
**Controller**: `HizmetBinalariController.cs` (1,520 bytes)

#### İşlevler:
- **Lokasyon Tanımlama**
- **Bina Bilgileri**
- **Departman Bağlantıları**
- **Adres Yönetimi**

## 🔄 Gerçek Zamanlı İletişim (SignalR)

### BankoHub - Banko İşlemleri
```csharp
// Gerçek zamanlı banko güncellemeleri
public class BankoHub : Hub
{
    // Sıra durumu değişiklikleri
    // Personel çağrıları
    // Müşteri bildirimleri
    // Sistem duyuruları
}
```

### TvHub - TV Ekranları
```csharp
// TV ekranları için gerçek zamanlı veri
public class TvHub : Hub
{
    // Sıra bilgileri güncellemesi
    // Duyuru yayını
    // Acil durum mesajları
    // Sistem durumu
}
```

## 📊 Veri Akış Şemaları

### 1. Kanal İşlem Akışı
```
Kullanıcı İsteği → KanalController → KanallarService → KanallarDal → Database
                                          ↓
AutoMapper ← KanallarDto ← Business Logic ← Entity Framework ← SQL Server
     ↓
JSON Response → Ajax → Frontend Update → UI Refresh
```

### 2. Banko Sıra Akışı
```
Müşteri Sıra Alımı → BankoController → SiralarService → SiralarDal → Database
                                            ↓
SignalR Hub ← Real-time Update ← Business Logic ← Entity Framework
     ↓
TV/Kiosk Screens ← WebSocket ← Hub Connection ← Client JavaScript
```

### 3. Personel Yetkilendirme Akışı
```
Login Request → LoginController → Authentication → UserInfoService
                     ↓                  ↓              ↓
              Session Create → Authorization → Claims/Roles → Controller Access
```

## 🎮 Kullanıcı Senaryoları

### Senaryo 1: Yeni Kanal Oluşturma
1. **Admin** kanal yönetimi sayfasına girer
2. "Yeni Kanal" butonuna tıklar
3. Kanal bilgilerini doldurur (Ad, Açıklama, Departman)
4. Kanal işlemlerini tanımlar
5. Alt işlemleri oluşturur
6. Personel ataması yapar
7. Kanal aktif hale gelir

### Senaryo 2: Müşteri Sıra Alma
1. **Müşteri** kiosk ekranına yaklaşır
2. Hizmet türünü seçer
3. Sıra numarası alır
4. TV ekranında sırasını takip eder
5. Sırası geldiğinde banko numarası gösterilir
6. İlgili bankoya gider

### Senaryo 3: Personel Banko Yönetimi
1. **Personel** sisteme giriş yapar
2. Atanmış bankosunu görür
3. Sıradaki müşteriyi çağırır
4. İşlem başlatır
5. İşlem tamamlandığında sonraki müşteriyi çağırır
6. Mola/kapanış durumunu günceller

## 🔧 API Endpoint'leri

### Kanal API'leri
```
GET    /Kanal/KanallarGetir/{id}
POST   /Kanal/KanallarEkle
PUT    /Kanal/KanallarGuncelle
DELETE /Kanal/KanallarSil/{id}
POST   /Kanal/PersonelAltKanalEslestirmeYap
```

### Banko API'leri
```
GET    /Banko/BankolarGetir
POST   /Banko/SiraAl
PUT    /Banko/SiraDurumGuncelle
GET    /Banko/BekleyenSiralar
POST   /Banko/MusteriCagir
```

### Personel API'leri
```
GET    /Personel/PersonellerListesi
POST   /Personel/PersonelEkle
PUT    /Personel/PersonelGuncelle
GET    /Personel/PersonelYetkileri/{id}
POST   /Personel/YetkiAta
```

## 🎨 UI/UX Akışları

### Dashboard Akışı
```
Login → Dashboard → Modül Seçimi → İşlem Sayfası → Sonuç → Dashboard
  ↓         ↓           ↓             ↓           ↓         ↓
Auth    Overview    Navigation    CRUD Ops    Feedback   Return
```

### Responsive Design
- **Desktop**: Tam özellikli yönetim paneli
- **Tablet**: Dokunmatik optimizasyonu
- **Mobile**: Temel işlemler ve görüntüleme
- **Kiosk**: Self-service arayüzü
- **TV**: Sadece görüntüleme, büyük fontlar

## 📈 Performans Metrikleri

### Sistem Kapasitesi
- **Eş Zamanlı Kullanıcı**: 500+ kullanıcı
- **Günlük İşlem**: 10,000+ işlem
- **Sıra Kapasitesi**: 1,000+ aktif sıra
- **Response Time**: <200ms (ortalama)

### Gerçek Zamanlı Güncellemeler
- **SignalR Bağlantı**: 100+ eş zamanlı
- **TV Güncelleme**: Her 5 saniye
- **Sıra Durumu**: Anlık güncelleme
- **Bildirimler**: <1 saniye gecikme

## 🔐 Güvenlik Akışları

### Yetkilendirme Matrisi
```
Admin     → Tüm modüllere erişim
Manager   → Departman modüllerine erişim
Personel  → Atanmış kanal/banko erişimi
Viewer    → Sadece görüntüleme yetkisi
```

### Audit Trail
- **Kullanıcı İşlemleri**: Tüm CRUD işlemleri loglanır
- **Sistem Değişiklikleri**: Konfigürasyon değişiklikleri
- **Hata Logları**: Exception ve error tracking
- **Performance Logs**: Yavaş sorgular ve bottleneck'ler

Bu modüler yapı, **SGK şubelerinin** tüm operasyonel ihtiyaçlarını karşılayacak şekilde tasarlanmıştır.
