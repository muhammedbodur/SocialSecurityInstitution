# ASP.NET MVC JavaScript İnceleme ve Refactor Raporu (TR)

Bu rapor; JavaScript kullanımını klasör yapısı, controller, view ve SignalR başlıkları altında ayrıntılı biçimde analiz eder. Amaç; kod tekrarlarını azaltmak, yönetilebilirliği artırmak ve ortak bir merkezden JS yönetimi sağlamaktır. Bu doküman iteratif olarak güncellenecektir ve yapılacak işler için kontrol listeleri içerir.

---

## 1) Klasör Yapısı Analizi

### 1.1 Mevcut Önemli Dosyalar
- `Views/Shared/_Layout.cshtml`
  - `~/js/common/script-loader.js` ile controller/action bazlı sayfa script yükleme deseni kullanılıyor.
  - `@await Html.PartialAsync("_Scripts")` ile global vendor ve ortak scriptler yükleniyor.
- `Views/Shared/_Scripts.cshtml`
  - Vendor: jQuery, Bootstrap, Toastr, Select2, ApexCharts, Perfect Scrollbar, vb.
  - SignalR kütüphanesi: `~/lib/microsoft/signalr/dist/browser/signalr.js`
  - Uygulama: `main.js`, `dashboards-analytics.js`, `ui-toasts.js`, `ui-popover.js`, `site.js`, `CallPanel.js`, `extended-ui-drag-and-drop.js`, `~/js/SignalR/Notification.js`
- `wwwroot/js/common/script-loader.js`
  - `/js/views/{controller}/{action}.js` yoluna göre sayfa-özel script yükleme; bağımlılık kontrolü (jquery/select2/bootstrap/toastr/swal) yapıyor.
- `wwwroot/js/SignalR/Notification.js`
  - `/dashboardHub` bağlantısı, yeniden bağlanma, ping, kalıcı uyarı, `ReceiveUpdates/ReceiveNotification/ReceiveError` eventleri, kullanıcı durum güncellemeleri.
- `wwwroot/js/SignalR/TvNotification.js`
  - `/TvHub` bağlantısı (URL param `TvId` ile), yeniden bağlanma, ping, kalıcı uyarı, TV ekran UI güncellemeleri, ses çalma ve kuyruk işleme.
- `wwwroot/js/SignalR/Utilities.js`
  - Cookie okuma helper (ES module export). Ortak SignalR yardımcıları burada değil.
- `wwwroot/js/site.js`
  - Boş/çok minimal. Ortak yardımcılar burada değil.

### 1.2 Tespit Edilen Sorunlar (Klasör Seviyesi)
- Vendor kütüphaneler bazı view’larda tekrar yükleniyor (CDN ve lokal karışık). Sürüm/çakışma ve yükleme sırası sorunu riski.
- ScriptLoader deseni var fakat sayfalar arasında tutarlı kullanılmıyor.
- SignalR istemci yardımcıları tekrarlı (alert/ping/reconnect mantığı birden fazla dosyada kopyalanmış).
- Ortak yardımcı modüller (DataTables, Select2, notify, ajax) merkezi değil.

### 1.3 Hedef Klasör Yapısı (Öneri)
- `wwwroot/js/core/`
  - `app.js` (global bootstrap + sayfa init dispatch)
  - `utils.js` (notify, fetch/ajax sarmalayıcı, anti-forgery, debounce, trNormalize)
  - `forms.js` (Select2 init, dependent dropdown, maskeler)
  - `tables.js` (DataTables varsayılanları, TR dil, export, arama)
- `wwwroot/js/views/{controller}/{action}.js`
  - Örn: `views/personel/listele.js`, `views/personel/ekle.js`, `views/personel/duzenle.js`
  - Örn: `views/tv/siralar.js`, `views/tv/listele.js`, `views/tv/siraac.js`, `views/tv/dragdropdemo.js`
- `wwwroot/js/signalr/`
  - `hub-core.js` (bağlantı oluşturma, start/retry, reconnect olayları)
  - `hub-alerts.js` (kalıcı uyarı göster/gizle)
  - `hub-ping.js` (standardize PingServer timer)
  - `hub-notify.js` (toastr kapsayıcı)
  - `notification.js` (dashboard’a özel event bağlayıcı; core’u kullanır)
  - `tv.js` (TV’ye özel event bağlayıcı ve UI etkileşimi; core’u kullanır)

### 1.4 Yapılacaklar — Klasör/Genel
- [ ] `core/` ve `views/` klasör hiyerarşisini oluştur.
- [ ] `signalr/` altında ortak helper’ları oluştur ve mevcut dosyaları bunları kullanacak şekilde sadeleştir.
- [ ] `site.js` yerine `core/*` modüllerini konumlandır ve kullan.
- [ ] ScriptLoader ile uyumlu dosya yollarını standartlaştır (`/js/views/{controller}/{action}.js`).

---

## 2) Layout ve Global Scriptler

### 2.1 `_Layout.cshtml`
- Controller/Action bilgisi alınarak ScriptLoader ile sayfa-özel script yüklenmesi yapılıyor (doğru yaklaşım).
- İyileştirme: Tüm sayfalar, sayfa-özel JS’i `views/...` altında konumlandırmalı ve ScriptLoader tarafından yüklenmeli. Inline `<script>` blokları azaltılmalı.

### 2.2 `_Scripts.cshtml`
- Global vendor ve ortak scriptlerin tek merkezden yüklenmesi doğru.
- İyileştirme: Vendor’lar sadece burada yüklensin. View’larda tekrarlı `<script src>` satırları kaldırılmalı. CDN/lokal karışıklığı giderilmeli (tercihen lokal paketler).

### 2.3 Yapılacaklar — Layout & Global
- [ ] View’larda yerel veya CDN vendor tekrarları tespit edilip kaldırılacak.
- [ ] ScriptLoader bağımlılık listeleri controller/action bazında gözden geçirilecek.
- [ ] Sayfa-özel tüm JS’ler `wwwroot/js/views/...` altına taşınacak.

---

## 3) Controller & View Bazlı Analiz

Aşağıda özellikle incelenen modüller listelenmiştir. Diğer controller’lar için aynı şablon genişletilecektir.

### 3.1 Personel Controller
- `Views/Personel/Listele.cshtml`
  - Inline JS: DataTables kurulumu, özel arama ve event binding mevcut.
  - Sorun: Inline ve tekrar edilebilir mantık (DataTables) view içinde duruyor.
  - Aksiyon: `wwwroot/js/views/personel/listele.js` oluşturulup inline script taşınacak. Ortak DataTables ayarları `core/tables.js`’e alınacak.
- `Views/Personel/Ekle.cshtml`
  - Form + Select2 kullanım alanı.
  - Aksiyon: `views/personel/ekle.js` + Select2/bağımlı dropdown `core/forms.js`.
- `Views/Personel/Duzenle.cshtml`
  - Form + Select2 kullanım alanı.
  - Aksiyon: `views/personel/duzenle.js` + ortak `core/forms.js`.

Yapılacaklar (Personel)
- [ ] `views/personel/listele.js` oluştur ve inline kodu taşı
- [ ] `views/personel/ekle.js` oluştur ve Select2 init’leri taşı
- [ ] `views/personel/duzenle.js` oluştur
- [ ] `core/tables.js` (DataTables varsayılanları) hazırla ve kullan
- [ ] `core/forms.js` (Select2/dependent) hazırla ve kullan

### 3.2 Tv Controller
- `Views/Tv/Siralar.cshtml`
  - Vendor tekrarları: jQuery, Toastr, Bootstrap, SignalR + CDN Bootstrap yeniden yükleniyor.
  - Ayrıca `~/js/SignalR/TvNotification.js` yükleniyor.
  - Aksiyon: Vendor tekrarlarını kaldır. `views/tv/siralar.js` oluştur, TV SignalR akışını buraya taşı, `signalr/hub-*.js` yardımcılarını kullan.
- `Views/Tv/Listele.cshtml`
  - CDN Bootstrap tekrarı var.
  - Aksiyon: Vendor tekrarı kaldır. `views/tv/listele.js` oluştur.
- `Views/Tv/SiraAc.cshtml`
  - CDN Bootstrap tekrarı var.
  - Aksiyon: Vendor tekrarı kaldır. `views/tv/siraac.js` oluştur.
- `Views/Tv/DragDropDemo.cshtml`
  - CDN Sortable kullanımı var.
  - Aksiyon: `views/tv/dragdropdemo.js` oluştur; ScriptLoader dependencies’e `sortablejs` ekle (gerekirse _Scripts’e lokal dahil et).

Yapılacaklar (Tv)
- [ ] `views/tv/siralar.js` oluştur, TV SignalR akışını buraya taşı
- [ ] `views/tv/listele.js` oluştur
- [ ] `views/tv/siraac.js` oluştur
- [ ] `views/tv/dragdropdemo.js` oluştur ve Sortable bağımlılığını yönet
- [ ] `Siralar/Listele/SiraAc` view’larından vendor `<script>` tekrarlarını kaldır

### 3.3 Diğer Controller’lar (Genel Şablon)
Her controller için aşağıdaki kontroller yapılacaktır:
- [ ] View içinde vendor `<script>` tekrarları var mı? (jQuery/Bootstrap/Toastr/Select2/SignalR)
- [ ] Inline script var mı? Varsa `wwwroot/js/views/{controller}/{action}.js` altına taşı.
- [ ] DataTables/Select2/Toastr vb. tekrar eden kurulumlar `core/*` modülleriyle değiştirildi mi?
- [ ] ScriptLoader bağımlılıkları doğru tanımlandı mı?

---

## 4) SignalR Yapısı

### 4.1 Mevcut
- `SignalR/Notification.js` (Dashboard)
  - `/dashboardHub` ile bağlantı, `ReceiveUpdates`, `ReceiveNotification`, `ReceiveError`, `OnConnected`, ping, reconnect, kalıcı uyarı, kullanıcı durum güncelleme, buton enable/disable.
- `SignalR/TvNotification.js` (TV)
  - `/TvHub` ile bağlantı (URL param `TvId`), ping, kalıcı uyarı, kuyruk işleme, UI güncelleme ve ses çalma, toggle animasyonları.
- `SignalR/Utilities.js`
  - Cookie helper (module). SignalR geneli için kullanılmıyor.

### 4.2 Sorunlar
- `showPersistentAlert`, `hidePersistentAlert`, ping ve reconnect akışları iki dosyada tekrarlı.
- Dashboard/Tv iş akışları ile bağlantı yaşam döngüsü aynı kurguyu kopyalıyor.

### 4.3 Hedef Mimari
- `signalr/hub-core.js`: `createHub(url, options)`, `startWithRetry`, `onReconnecting`, `onReconnected`, `onClose` — tek merkez.
- `signalr/hub-alerts.js`: `showPersistentAlert`, `hidePersistentAlert` — tek merkez.
- `signalr/hub-ping.js`: `startPing(connection, method, intervalMs)` — tek merkez.
- `signalr/hub-notify.js`: `notify.info/success/error` (toastr sarmalayıcı) — tek merkez.
- Sayfa modülleri (dashboard ve tv) sadece kendilerine özgü `connection.on(...)` event işleyicilerini içerir.

### 4.4 Yapılacaklar — SignalR
- [ ] `hub-core.js`, `hub-alerts.js`, `hub-ping.js`, `hub-notify.js` dosyalarını oluştur
- [ ] `Notification.js` ve `TvNotification.js` dosyalarını bu yardımcıları kullanacak şekilde sadeleştir
- [ ] `Tv` sayfalarındaki UI güncellemelerini `views/tv/*.js` modüllerine taşı
- [ ] SignalR kütüphanesinin (signalr.js) sadece `/_Scripts.cshtml` içinde yüklendiğini doğrula

---

## 5) Ortak Yardımcı Modüller

### 5.1 `core/utils.js`
- `notify.success/error/info`
- `ajax`/`fetch` sarmalayıcı, anti-forgery token ekleme
- `debounce`/`throttle`
- Türkçe arama için `trNormalize`

### 5.2 `core/forms.js`
- `initSelect2(selector, options)`
- `bindDependentSelect(parent, child, urlBuilder)`
- Maske ve tarih yardımcıları (varsa)

### 5.3 `core/tables.js`
- `initDataTable(selector, options)`; TR dil, butonlar, ilk kolon sırasız vb. varsayılanlar
- Ortak arama kutusu bağlama, export, responsive ayarları

### 5.4 Yapılacaklar — Core
- [ ] `core/utils.js` oluştur ve toastr/anti-forgery/normalize fonksiyonlarını ekle
- [ ] `core/forms.js` oluştur ve Select2/dependent kurulumlarını merkezileştir
- [ ] `core/tables.js` oluştur ve DataTables varsayılanlarını merkezileştir
- [ ] Sayfa modüllerini bu core fonksiyonlarını kullanacak şekilde güncelle

---

## 6) Uygulama Planı (Adım Adım)
1. Altyapı
   - [ ] `core/` ve `signalr/` modüllerini oluştur
   - [ ] ScriptLoader bağımlılık eşleştirmelerini gözden geçir
2. Tv Modülü (yüksek görünürlük)
   - [ ] `views/tv/siralar.js` + vendor tekrarlarını kaldır
   - [ ] `views/tv/listele.js`, `views/tv/siraac.js`, `views/tv/dragdropdemo.js`
   - [ ] `TvNotification.js` refactor: `signalr/hub-*` yardımcılarını kullan
3. Personel Modülü
   - [ ] `views/personel/listele.js` (inline DataTables kodunu taşı)
   - [ ] `views/personel/{ekle,duzenle}.js` (Select2/dependent)
   - [ ] `core/tables.js` ve `core/forms.js` entegrasyonu
4. Diğer Controller’lar
   - [ ] Inline ve vendor tekrarlarını temizle, `views/...` modülleri oluştur
5. Son Temizlik
   - [ ] `site.js` kullanımını `core/*` lehine azalt/temizle
   - [ ] Dökümantasyonu güncelle (bu rapor)

---

## 7) Kontrol Listeleri (Hızlı Denetim)

### 7.1 View Denetimi
- [ ] View içinde vendor `<script src>` var mı? (jQuery/Bootstrap/Toastr/Select2/SignalR)
- [ ] Inline `<script>` var mı? — Taşındı mı `wwwroot/js/views/{controller}/{action}.js`?
- [ ] `@section Scripts` sadece sayfa-özel dosyayı içeriyor mu? (Gerekirse boş)
- [ ] CDN ve lokal aynı anda kullanılıyor mu? — Tek kaynağa indirildi mi?

### 7.2 ScriptLoader Denetimi
- [ ] `views/{controller}/{action}.js` yolu ScriptLoader beklentisiyle uyumlu mu?
- [ ] Gerekli bağımlılıklar (jquery/select2/bootstrap/toastr) doğru listelendi mi?

### 7.3 SignalR Denetimi
- [ ] `signalr.js` sadece `/_Scripts.cshtml` içinde mi yükleniyor?
- [ ] `hub-core/alerts/ping/notify` yardımcıları kullanılıyor mu?
- [ ] `Notification.js`/`TvNotification.js` içindeki tekrarlar temizlendi mi?

### 7.4 Core Modüller Denetimi
- [ ] DataTables kurulumları `core/tables.js` ile yapılıyor mu?
- [ ] Select2/dependent kurulumları `core/forms.js` ile mi?
- [ ] Toastr/notify ve ajax sarmalayıcı `core/utils.js` ile mi?

---

## 8) Bulunan Tekrarlar (Örnekler)
- `Views/Tv/Siralar.cshtml` içinde:
  - `~/vendor/libs/jquery/jquery.js`, `~/vendor/libs/toastr/toastr.js`, `~/vendor/js/bootstrap.js`, `~/lib/microsoft/signalr/dist/browser/signalr.js` + `https://cdn.jsdelivr.net/.../bootstrap.bundle.min.js` tekrarları.
- `Views/Tv/Listele.cshtml` ve `Views/Tv/SiraAc.cshtml` içinde CDN Bootstrap tekrarları.
- `Views/Personel/Listele.cshtml` içinde inline DataTables kurulumu.

Not: Diğer controller/view’lar için aynı tarama yaklaşımı ile liste genişletilecek.
Bu rapor proje kökünde `SocialSecurityInstitution.PresentationLayer/docs/raporlar/javascript/JS_Refactor_Raporu_TR.md` yolundan erişilebilir. Birlikte ilerledikçe maddeleri işaretleyip raporu güncelleyeceğiz.
