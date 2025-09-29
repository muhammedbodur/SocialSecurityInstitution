# Data Access Layer (Veri Erişim Katmanı) Şeması

## 📁 Klasör Yapısı

```
SocialSecurityInstitution.DataAccessLayer/
├── 📁 AbstractDataServices/ (36 dosya)
│   ├── 📄 IAtanmaNedenleriDal.cs
│   ├── 📄 IBankolarDal.cs
│   ├── 📄 IDepartmanlarDal.cs
│   ├── 📄 IHizmetBinalariDal.cs
│   ├── 📄 IKanallarDal.cs
│   ├── 📄 IKanallarAltDal.cs
│   ├── 📄 IKanalIslemleriDal.cs
│   ├── 📄 IKanalAltIslemleriDal.cs
│   ├── 📄 IKanalPersonelleriDal.cs
│   ├── 📄 IPersonellerDal.cs
│   ├── 📄 IServislerDal.cs
│   ├── 📄 ISiralarDal.cs
│   ├── 📄 IUnvanlarDal.cs
│   ├── 📄 IYetkilerDal.cs
│   └── ... (22 adet daha)
├── 📁 ConcreteDataServices/ (37 dosya)
│   ├── 📄 AtanmaNedenleriDal.cs
│   ├── 📄 BankolarDal.cs
│   ├── 📄 DepartmanlarDal.cs
│   ├── 📄 HizmetBinalariDal.cs
│   ├── 📄 KanallarDal.cs
│   ├── 📄 KanallarAltDal.cs
│   ├── 📄 KanalIslemleriDal.cs
│   ├── 📄 KanalAltIslemleriDal.cs
│   ├── 📄 KanalPersonelleriDal.cs
│   ├── 📄 PersonellerDal.cs
│   ├── 📄 ServislerDal.cs
│   ├── 📄 SiralarDal.cs
│   ├── 📄 UnvanlarDal.cs
│   ├── 📄 YetkilerDal.cs
│   └── ... (23 adet daha)
├── 📁 ConcreteDataBase/ (1 dosya)
│   └── 📄 Context.cs
├── 📁 Migrations/ (13 dosya)
│   ├── 📄 20240101000000_InitialCreate.cs
│   ├── 📄 20240115000000_AddKanalTables.cs
│   ├── 📄 20240201000000_AddPersonelTables.cs
│   ├── 📄 20240215000000_AddBankoTables.cs
│   ├── 📄 20240301000000_AddDepartmanTables.cs
│   ├── 📄 20240315000000_AddYetkiTables.cs
│   ├── 📄 20240401000000_AddHizmetBinasiTables.cs
│   ├── 📄 20240415000000_AddServisTables.cs
│   ├── 📄 20240501000000_AddSiraTables.cs
│   ├── 📄 20240515000000_AddUnvanTables.cs
│   ├── 📄 20240601000000_AddAtamaNedeniTables.cs
│   ├── 📄 20240615000000_UpdateRelations.cs
│   └── 📄 ContextModelSnapshot.cs
├── 📄 SocialSecurityInstitution.DataAccessLayer.csproj
└── 📄 Yardımcı Doküman.txt
```

## 🏗️ Repository Pattern Mimarisi

### 1. Abstract Data Services (Repository Interfaces)
**Amaç**: Veri erişim sözleşmeleri
- **36 farklı repository interface**
- **Generic CRUD operations**
- **Entity-specific methods**

#### Örnek Repository Interface:
```csharp
public interface IKanallarDal : IGenericDal<Kanallar>
{
    Task<List<Kanallar>> GetKanallarByDepartmanIdAsync(int departmanId);
    Task<List<Kanallar>> GetAktifKanallarAsync();
    Task<bool> KanalVarMiAsync(string kanalAdi);
    Task<int> GetKanalSayisiAsync();
}
```

### 2. Concrete Data Services (Repository Implementations)
**Amaç**: Repository interface'lerinin gerçek implementasyonları
- **37 farklı repository sınıfı**
- **Entity Framework Core kullanımı**
- **LINQ sorguları**
- **Async operations**

#### Örnek Repository Implementation:
```csharp
public class KanallarDal : GenericRepository<Kanallar>, IKanallarDal
{
    public KanallarDal(Context context) : base(context) { }
    
    public async Task<List<Kanallar>> GetKanallarByDepartmanIdAsync(int departmanId)
    {
        return await _context.Kanallar
            .Where(k => k.DepartmanId == departmanId && k.Aktif)
            .ToListAsync();
    }
    
    public async Task<List<Kanallar>> GetAktifKanallarAsync()
    {
        return await _context.Kanallar
            .Where(k => k.Aktif)
            .OrderBy(k => k.KanalAdi)
            .ToListAsync();
    }
}
```

## 🗄️ Database Context

### Context.cs
```csharp
public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }
    
    // DbSet Tanımları
    public DbSet<Kanallar> Kanallar { get; set; }
    public DbSet<KanallarAlt> KanallarAlt { get; set; }
    public DbSet<KanalIslemleri> KanalIslemleri { get; set; }
    public DbSet<KanalAltIslemleri> KanalAltIslemleri { get; set; }
    public DbSet<KanalPersonelleri> KanalPersonelleri { get; set; }
    public DbSet<Personeller> Personeller { get; set; }
    public DbSet<Bankolar> Bankolar { get; set; }
    public DbSet<Departmanlar> Departmanlar { get; set; }
    public DbSet<HizmetBinalari> HizmetBinalari { get; set; }
    public DbSet<Servisler> Servisler { get; set; }
    public DbSet<Siralar> Siralar { get; set; }
    public DbSet<Unvanlar> Unvanlar { get; set; }
    public DbSet<Yetkiler> Yetkiler { get; set; }
    public DbSet<AtanmaNedenleri> AtanmaNedenleri { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity Configurations
        // Relationships
        // Constraints
        // Indexes
    }
}
```

## 📊 Migration Geçmişi

### Database Evolution Timeline
1. **InitialCreate** - Temel tablolar
2. **AddKanalTables** - Kanal sistemi
3. **AddPersonelTables** - Personel yönetimi
4. **AddBankoTables** - Banko sistemi
5. **AddDepartmanTables** - Departman yapısı
6. **AddYetkiTables** - Yetkilendirme
7. **AddHizmetBinasiTables** - Lokasyon yönetimi
8. **AddServisTables** - Servis tanımları
9. **AddSiraTables** - Sıra yönetimi
10. **AddUnvanTables** - Unvan sistemi
11. **AddAtamaNedeniTables** - Atama nedenleri
12. **UpdateRelations** - İlişki güncellemeleri
13. **ContextModelSnapshot** - Mevcut durum

## 🔗 Entity İlişkileri

### Ana Entity'ler ve İlişkileri:

#### 1. Kanal Sistemi
```
Kanallar (1) ←→ (N) KanallarAlt
    ↓ (1)
    ↓
KanalIslemleri (1) ←→ (N) KanalAltIslemleri
    ↓ (1)
    ↓
KanalPersonelleri (N) ←→ (1) Personeller
```

#### 2. Organizasyon Yapısı
```
HizmetBinalari (1) ←→ (N) Departmanlar
    ↓ (1)
    ↓
Departmanlar (1) ←→ (N) Personeller
    ↓ (1)
    ↓
Personeller (1) ←→ (N) Bankolar
```

#### 3. Sıra Yönetimi
```
Bankolar (1) ←→ (N) Siralar
    ↓ (1)
    ↓
Siralar (N) ←→ (1) Servisler
```

## 🔧 Generic Repository Pattern

### IGenericDal<T> Interface:
```csharp
public interface IGenericDal<T> where T : class
{
    Task<List<T>> TGetAllAsync();
    Task<T> TGetByIdAsync(int id);
    Task<bool> TAddAsync(T entity);
    Task<bool> TUpdateAsync(T entity);
    Task<bool> TDeleteAsync(T entity);
    Task<bool> TDeleteByIdAsync(int id);
    Task<int> TCountAsync();
    Task<bool> TAnyAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> TGetFilteredListAsync(Expression<Func<T, bool>> predicate);
}
```

### GenericRepository<T> Implementation:
```csharp
public class GenericRepository<T> : IGenericDal<T> where T : class
{
    protected readonly Context _context;
    private readonly DbSet<T> _dbSet;
    
    public GenericRepository(Context context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    public async Task<List<T>> TGetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    public async Task<T> TGetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    // ... diğer CRUD operations
}
```

## 🎯 Özel Repository Methodları

### Kanal Repository Özel Methodları:
- `GetKanallarByDepartmanIdAsync()`
- `GetAktifKanallarAsync()`
- `KanalVarMiAsync()`
- `GetKanalSayisiAsync()`

### Personel Repository Özel Methodları:
- `GetPersonellerByDepartmanAsync()`
- `GetAktifPersonellerAsync()`
- `GetPersonelByTcKimlikAsync()`
- `GetPersonelYetkilerAsync()`

### Banko Repository Özel Methodları:
- `GetBankolarByHizmetBinasiAsync()`
- `GetAktifBankolarAsync()`
- `GetBankoSiraSayisiAsync()`
- `GetBekleyenSiralarAsync()`

## 🔐 Veri Güvenliği

### Connection String Security
- **Encrypted connection strings**
- **Environment-based configuration**
- **Secure credential management**

### SQL Injection Prevention
- **Parameterized queries**
- **LINQ to Entities**
- **Input validation**

### Audit Trail
- **Created/Updated timestamps**
- **User tracking**
- **Change logging**

## 📈 Performans Optimizasyonları

### Query Optimization
- **Eager loading** (`Include()`)
- **Lazy loading** configuration
- **Projection** (`Select()`)
- **Filtering** (`Where()`)

### Indexing Strategy
- **Primary keys**
- **Foreign keys**
- **Frequently queried columns**
- **Composite indexes**

### Connection Management
- **Connection pooling**
- **DbContext lifecycle**
- **Async operations**

## 🔄 Transaction Management

### Unit of Work Pattern
```csharp
public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    
    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }
    
    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }
}
```

## 📊 Veritabanı İstatistikleri

| Entity | Estimated Records | Primary Use |
|--------|------------------|-------------|
| Personeller | 500-1000 | Çalışan bilgileri |
| Kanallar | 50-100 | Hizmet kanalları |
| KanalIslemleri | 200-500 | Kanal işlemleri |
| Bankolar | 100-200 | Banko noktaları |
| Siralar | 10000+ | Günlük sıra kayıtları |
| Departmanlar | 20-50 | Organizasyon |
| HizmetBinalari | 5-10 | Lokasyonlar |

Bu katman, **veritabanı ile uygulama arasındaki köprü** görevi görür ve tüm veri işlemlerini merkezi olarak yönetir.
