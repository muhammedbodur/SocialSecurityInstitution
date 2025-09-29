# Veritabanı Şeması ve İlişkiler

## 🗄️ Ana Tablolar ve İlişkiler

### 1. Organizasyon Tabloları

#### HizmetBinalari (Hizmet Binaları)
```sql
CREATE TABLE HizmetBinalari (
    HizmetBinasiId INT IDENTITY(1,1) PRIMARY KEY,
    HizmetBinasiAdi NVARCHAR(100) NOT NULL,
    Adres NVARCHAR(200),
    Telefon NVARCHAR(20),
    Aktif BIT NOT NULL DEFAULT 1,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    GuncellemeTarihi DATETIME2
);
```

#### Departmanlar
```sql
CREATE TABLE Departmanlar (
    DepartmanId INT IDENTITY(1,1) PRIMARY KEY,
    DepartmanAdi NVARCHAR(100) NOT NULL,
    Aciklama NVARCHAR(500),
    HizmetBinasiId INT NOT NULL,
    Aktif BIT NOT NULL DEFAULT 1,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    FOREIGN KEY (HizmetBinasiId) REFERENCES HizmetBinalari(HizmetBinasiId)
);
```

#### Unvanlar
```sql
CREATE TABLE Unvanlar (
    UnvanId INT IDENTITY(1,1) PRIMARY KEY,
    UnvanAdi NVARCHAR(100) NOT NULL,
    Aciklama NVARCHAR(500),
    Aktif BIT NOT NULL DEFAULT 1
);
```

### 2. Personel Tabloları

#### Personeller
```sql
CREATE TABLE Personeller (
    PersonelId INT IDENTITY(1,1) PRIMARY KEY,
    Ad NVARCHAR(50) NOT NULL,
    Soyad NVARCHAR(50) NOT NULL,
    TcKimlikNo NVARCHAR(11) NOT NULL UNIQUE,
    Email NVARCHAR(100),
    Telefon NVARCHAR(20),
    DepartmanId INT NOT NULL,
    UnvanId INT NOT NULL,
    Aktif BIT NOT NULL DEFAULT 1,
    IseBaslamaTarihi DATE NOT NULL,
    IstenAyrılmaTarihi DATE,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    FOREIGN KEY (DepartmanId) REFERENCES Departmanlar(DepartmanId),
    FOREIGN KEY (UnvanId) REFERENCES Unvanlar(UnvanId)
);
```

#### Yetkiler
```sql
CREATE TABLE Yetkiler (
    YetkiId INT IDENTITY(1,1) PRIMARY KEY,
    YetkiAdi NVARCHAR(100) NOT NULL,
    YetkiAciklamasi NVARCHAR(500),
    ModulAdi NVARCHAR(100),
    Aktif BIT NOT NULL DEFAULT 1
);
```

#### PersonelYetkileri (Many-to-Many)
```sql
CREATE TABLE PersonelYetkileri (
    PersonelYetkiId INT IDENTITY(1,1) PRIMARY KEY,
    PersonelId INT NOT NULL,
    YetkiId INT NOT NULL,
    AtamaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    AtayanPersonelId INT,
    
    FOREIGN KEY (PersonelId) REFERENCES Personeller(PersonelId),
    FOREIGN KEY (YetkiId) REFERENCES Yetkiler(YetkiId),
    FOREIGN KEY (AtayanPersonelId) REFERENCES Personeller(PersonelId)
);
```

### 3. Kanal Sistemi Tabloları

#### Kanallar
```sql
CREATE TABLE Kanallar (
    KanalId INT IDENTITY(1,1) PRIMARY KEY,
    KanalAdi NVARCHAR(100) NOT NULL,
    KanalAciklamasi NVARCHAR(500),
    DepartmanId INT NOT NULL,
    Aktif BIT NOT NULL DEFAULT 1,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    GuncellemeTarihi DATETIME2,
    OlusturanKullanici NVARCHAR(50),
    GuncelleyenKullanici NVARCHAR(50),
    
    FOREIGN KEY (DepartmanId) REFERENCES Departmanlar(DepartmanId)
);
```

#### KanallarAlt
```sql
CREATE TABLE KanallarAlt (
    KanalAltId INT IDENTITY(1,1) PRIMARY KEY,
    KanalAltAdi NVARCHAR(100) NOT NULL,
    KanalAltAciklamasi NVARCHAR(500),
    KanalId INT NOT NULL,
    Aktif BIT NOT NULL DEFAULT 1,
    Sira INT NOT NULL DEFAULT 0,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    FOREIGN KEY (KanalId) REFERENCES Kanallar(KanalId)
);
```

#### KanalIslemleri
```sql
CREATE TABLE KanalIslemleri (
    KanalIslemId INT IDENTITY(1,1) PRIMARY KEY,
    IslemAdi NVARCHAR(100) NOT NULL,
    IslemAciklamasi NVARCHAR(500),
    KanalId INT NOT NULL,
    Aktif BIT NOT NULL DEFAULT 1,
    Sira INT NOT NULL DEFAULT 0,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    FOREIGN KEY (KanalId) REFERENCES Kanallar(KanalId)
);
```

#### KanalAltIslemleri
```sql
CREATE TABLE KanalAltIslemleri (
    KanalAltIslemId INT IDENTITY(1,1) PRIMARY KEY,
    AltIslemAdi NVARCHAR(100) NOT NULL,
    AltIslemAciklamasi NVARCHAR(500),
    KanalIslemId INT,
    Aktif BIT NOT NULL DEFAULT 1,
    Sira INT NOT NULL DEFAULT 0,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    FOREIGN KEY (KanalIslemId) REFERENCES KanalIslemleri(KanalIslemId)
);
```

#### KanalPersonelleri (Many-to-Many)
```sql
CREATE TABLE KanalPersonelleri (
    KanalPersonelId INT IDENTITY(1,1) PRIMARY KEY,
    KanalId INT NOT NULL,
    PersonelId INT NOT NULL,
    AtamaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    AtayanPersonelId INT,
    Aktif BIT NOT NULL DEFAULT 1,
    
    FOREIGN KEY (KanalId) REFERENCES Kanallar(KanalId),
    FOREIGN KEY (PersonelId) REFERENCES Personeller(PersonelId),
    FOREIGN KEY (AtayanPersonelId) REFERENCES Personeller(PersonelId)
);
```

### 4. Banko ve Sıra Sistemi Tabloları

#### Bankolar
```sql
CREATE TABLE Bankolar (
    BankoId INT IDENTITY(1,1) PRIMARY KEY,
    BankoAdi NVARCHAR(50) NOT NULL,
    HizmetBinasiId INT NOT NULL,
    PersonelId INT,
    Aktif BIT NOT NULL DEFAULT 1,
    MaksimumSiraSayisi INT NOT NULL DEFAULT 50,
    CalismaSaatiBaslangic TIME NOT NULL,
    CalismaSaatiBitis TIME NOT NULL,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    FOREIGN KEY (HizmetBinasiId) REFERENCES HizmetBinalari(HizmetBinasiId),
    FOREIGN KEY (PersonelId) REFERENCES Personeller(PersonelId)
);
```

#### Servisler
```sql
CREATE TABLE Servisler (
    ServisId INT IDENTITY(1,1) PRIMARY KEY,
    ServisAdi NVARCHAR(100) NOT NULL,
    ServisAciklamasi NVARCHAR(500),
    Aktif BIT NOT NULL DEFAULT 1,
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE()
);
```

#### Siralar
```sql
CREATE TABLE Siralar (
    SiraId INT IDENTITY(1,1) PRIMARY KEY,
    SiraNumarasi NVARCHAR(10) NOT NULL,
    BankoId INT NOT NULL,
    ServisId INT NOT NULL,
    MusteriAdSoyad NVARCHAR(100),
    MusteriTelefon NVARCHAR(20),
    SiraDurumu INT NOT NULL DEFAULT 1, -- 1:Bekliyor, 2:Çağrıldı, 3:İşleniyor, 4:Tamamlandı, 5:İptal
    OlusturmaTarihi DATETIME2 NOT NULL DEFAULT GETDATE(),
    CagrilmaTarihi DATETIME2,
    BaslamaTarihi DATETIME2,
    BitisTarihi DATETIME2,
    
    FOREIGN KEY (BankoId) REFERENCES Bankolar(BankoId),
    FOREIGN KEY (ServisId) REFERENCES Servisler(ServisId)
);
```

### 5. Atama ve Yardımcı Tablolar

#### AtanmaNedenleri
```sql
CREATE TABLE AtanmaNedenleri (
    AtamaNedeniId INT IDENTITY(1,1) PRIMARY KEY,
    AtamaNedeniAdi NVARCHAR(100) NOT NULL,
    Aciklama NVARCHAR(500),
    Aktif BIT NOT NULL DEFAULT 1
);
```

## 🔗 İlişki Diyagramı

```
HizmetBinalari (1) ←→ (N) Departmanlar (1) ←→ (N) Personeller
       ↓                                              ↓
       ↓                                              ↓
   Bankolar (N) ←→ (1) Personeller              KanalPersonelleri
       ↓                                              ↓
       ↓                                              ↓
   Siralar (N) ←→ (1) Servisler              Kanallar (1) ←→ (N) KanallarAlt
                                                     ↓
                                                     ↓
                                             KanalIslemleri (1) ←→ (N) KanalAltIslemleri
```

## 📊 İndeksler ve Performans

### Primary Key İndeksleri
```sql
-- Otomatik olarak oluşturulan clustered index'ler
PK_HizmetBinalari_HizmetBinasiId
PK_Departmanlar_DepartmanId
PK_Personeller_PersonelId
PK_Kanallar_KanalId
PK_Bankolar_BankoId
PK_Siralar_SiraId
```

### Foreign Key İndeksleri
```sql
-- Performans için eklenen non-clustered index'ler
CREATE INDEX IX_Departmanlar_HizmetBinasiId ON Departmanlar(HizmetBinasiId);
CREATE INDEX IX_Personeller_DepartmanId ON Personeller(DepartmanId);
CREATE INDEX IX_Personeller_UnvanId ON Personeller(UnvanId);
CREATE INDEX IX_Kanallar_DepartmanId ON Kanallar(DepartmanId);
CREATE INDEX IX_KanallarAlt_KanalId ON KanallarAlt(KanalId);
CREATE INDEX IX_KanalIslemleri_KanalId ON KanalIslemleri(KanalId);
CREATE INDEX IX_KanalAltIslemleri_KanalIslemId ON KanalAltIslemleri(KanalIslemId);
CREATE INDEX IX_Bankolar_HizmetBinasiId ON Bankolar(HizmetBinasiId);
CREATE INDEX IX_Bankolar_PersonelId ON Bankolar(PersonelId);
CREATE INDEX IX_Siralar_BankoId ON Siralar(BankoId);
CREATE INDEX IX_Siralar_ServisId ON Siralar(ServisId);
```

### Özel İndeksler
```sql
-- Sık kullanılan sorgular için
CREATE INDEX IX_Personeller_TcKimlikNo ON Personeller(TcKimlikNo);
CREATE INDEX IX_Personeller_Aktif ON Personeller(Aktif);
CREATE INDEX IX_Siralar_SiraDurumu ON Siralar(SiraDurumu);
CREATE INDEX IX_Siralar_OlusturmaTarihi ON Siralar(OlusturmaTarihi);
CREATE INDEX IX_Kanallar_Aktif ON Kanallar(Aktif);
CREATE INDEX IX_Bankolar_Aktif ON Bankolar(Aktif);
```

## 🔧 Stored Procedure'lar

### Sıra Yönetimi
```sql
CREATE PROCEDURE sp_SiraAl
    @BankoId INT,
    @ServisId INT,
    @MusteriAdSoyad NVARCHAR(100) = NULL,
    @MusteriTelefon NVARCHAR(20) = NULL
AS
BEGIN
    DECLARE @SiraNumarasi NVARCHAR(10);
    DECLARE @SonSiraNo INT;
    
    -- Son sıra numarasını al
    SELECT @SonSiraNo = ISNULL(MAX(CAST(RIGHT(SiraNumarasi, 3) AS INT)), 0)
    FROM Siralar 
    WHERE BankoId = @BankoId 
    AND CAST(OlusturmaTarihi AS DATE) = CAST(GETDATE() AS DATE);
    
    -- Yeni sıra numarası oluştur
    SET @SiraNumarasi = 'B' + CAST(@BankoId AS NVARCHAR(2)) + '-' + RIGHT('000' + CAST(@SonSiraNo + 1 AS NVARCHAR(3)), 3);
    
    -- Sıra ekle
    INSERT INTO Siralar (SiraNumarasi, BankoId, ServisId, MusteriAdSoyad, MusteriTelefon, SiraDurumu)
    VALUES (@SiraNumarasi, @BankoId, @ServisId, @MusteriAdSoyad, @MusteriTelefon, 1);
    
    SELECT @SiraNumarasi AS SiraNumarasi;
END
```

### Banko İstatistikleri
```sql
CREATE PROCEDURE sp_BankoIstatistikleri
    @BankoId INT,
    @Tarih DATE = NULL
AS
BEGIN
    IF @Tarih IS NULL SET @Tarih = CAST(GETDATE() AS DATE);
    
    SELECT 
        b.BankoAdi,
        p.Ad + ' ' + p.Soyad AS PersonelAdSoyad,
        COUNT(CASE WHEN s.SiraDurumu = 1 THEN 1 END) AS BekleyenSiraSayisi,
        COUNT(CASE WHEN s.SiraDurumu = 4 THEN 1 END) AS TamamlananSiraSayisi,
        COUNT(CASE WHEN s.SiraDurumu = 5 THEN 1 END) AS IptalEdilenSiraSayisi,
        AVG(CASE 
            WHEN s.SiraDurumu = 4 AND s.BitisTarihi IS NOT NULL 
            THEN DATEDIFF(MINUTE, s.OlusturmaTarihi, s.BitisTarihi) 
        END) AS OrtalamaBeklemeZamaniDakika
    FROM Bankolar b
    LEFT JOIN Personeller p ON b.PersonelId = p.PersonelId
    LEFT JOIN Siralar s ON b.BankoId = s.BankoId AND CAST(s.OlusturmaTarihi AS DATE) = @Tarih
    WHERE b.BankoId = @BankoId
    GROUP BY b.BankoAdi, p.Ad, p.Soyad;
END
```

## 🔐 Güvenlik ve Kısıtlamalar

### Check Constraints
```sql
-- Sıra durumu kontrolü
ALTER TABLE Siralar 
ADD CONSTRAINT CK_Siralar_SiraDurumu 
CHECK (SiraDurumu IN (1, 2, 3, 4, 5));

-- TC Kimlik No formatı
ALTER TABLE Personeller 
ADD CONSTRAINT CK_Personeller_TcKimlikNo 
CHECK (TcKimlikNo LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]');

-- Çalışma saati kontrolü
ALTER TABLE Bankolar 
ADD CONSTRAINT CK_Bankolar_CalismaSaati 
CHECK (CalismaSaatiBaslangic < CalismaSaatiBitis);
```

### Triggers
```sql
-- Personel silme kontrolü
CREATE TRIGGER tr_Personeller_Delete
ON Personeller
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Personeller 
    SET Aktif = 0, IstenAyrılmaTarihi = GETDATE()
    WHERE PersonelId IN (SELECT PersonelId FROM deleted);
END
```

## 📈 Veri Büyüklüğü Tahminleri

| Tablo | Tahmini Kayıt Sayısı | Günlük Artış | Aylık Boyut |
|-------|---------------------|---------------|-------------|
| HizmetBinalari | 5-10 | 0 | Sabit |
| Departmanlar | 20-50 | 0-1 | Minimal |
| Personeller | 500-1000 | 1-5 | 50KB |
| Kanallar | 50-100 | 0-2 | 10KB |
| KanalIslemleri | 200-500 | 5-10 | 25KB |
| Bankolar | 100-200 | 0-1 | 5KB |
| **Siralar** | **10000+** | **500-1000** | **5MB** |
| KanalPersonelleri | 1000-2000 | 10-20 | 100KB |

## 🔄 Backup ve Maintenance

### Backup Stratejisi
```sql
-- Günlük full backup
BACKUP DATABASE SocialSecurityDB 
TO DISK = 'C:\Backup\SocialSecurityDB_Full.bak'
WITH COMPRESSION, CHECKSUM;

-- Saatlik transaction log backup
BACKUP LOG SocialSecurityDB 
TO DISK = 'C:\Backup\SocialSecurityDB_Log.trn'
WITH COMPRESSION;
```

### Maintenance Jobs
```sql
-- Index reorganization (haftalık)
ALTER INDEX ALL ON Siralar REORGANIZE;

-- Statistics update (günlük)
UPDATE STATISTICS Siralar;

-- Eski sıra kayıtlarını arşivleme (aylık)
DELETE FROM Siralar 
WHERE OlusturmaTarihi < DATEADD(MONTH, -6, GETDATE())
AND SiraDurumu IN (4, 5); -- Tamamlanan ve iptal edilen
```

Bu veritabanı şeması, **yüksek performanslı** ve **ölçeklenebilir** bir SGK yönetim sistemi için optimize edilmiştir.
