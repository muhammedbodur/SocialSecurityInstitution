using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.BusinessObjectLayer.DataBaseEntities;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Personeller> Personeller { get; set; }
        public DbSet<PersonelCocuklari> PersonelCocuklari { get; set; }
        public DbSet<AtanmaNedenleri> AtanmaNedenleri { get; set; }
        public DbSet<BankoIslemleri> BankoIslemleri { get; set; }
        public DbSet<Bankolar> Bankolar { get; set; }
        public DbSet<BankolarKullanici> BankolarKullanici { get; set; }
        public DbSet<Departmanlar> Departmanlar { get; set; }
        public DbSet<HizmetBinalari> HizmetBinalari { get; set; }
        public DbSet<Kanallar> Kanallar { get; set; }
        public DbSet<KanallarAlt> KanallarAlt { get; set; }
        public DbSet<KanalAltIslemleri> KanalAltIslemleri { get; set; }
        public DbSet<KanalIslemleri> KanalIslemleri { get; set; }
        public DbSet<KanalPersonelleri> KanalPersonelleri { get; set; }
        public DbSet<PdksCihazlar> PdksCiazlar { get; set; }
        public DbSet<Servisler> Servisler { get; set; }
        public DbSet<Unvanlar> Unvanlar { get; set; }
        public DbSet<Sendikalar> Sendikalar { get; set; }
        public DbSet<Yetkiler> Yetkiler { get; set; }
        public DbSet<Iller> Iller { get; set; }
        public DbSet<Ilceler> Ilceler { get; set; }
        public DbSet<LoginLogoutLog> LoginLogoutLog { get; set; }
        public DbSet<Moduller> Modul { get; set; }
        public DbSet<ModulController> ModulController { get; set; }
        public DbSet<ModulControllerIslemler> ModulControllerIslemler { get; set; }
        public DbSet<PersonelYetkileri> PersonelYetkileri { get; set; }
        public DbSet<KioskGruplari> KioskGruplari { get; set; }
        public DbSet<KioskIslemGruplari> KioskIslemGruplari { get; set; }
        public DbSet<Siralar> Siralar { get; set; }
        public DbSet<DatabaseLog> DatabaseLog { get; set; }
        public DbSet<HubConnection> HubConnection { get; set; }
        public DbSet<HubTvConnection> HubTvConnection { get; set; }
        public DbSet<Tvler> Tvler { get; set; }
        public DbSet<TvBankolari> TvBankolari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply SM_ prefix to all tables
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).ToTable("SM_" + entityType.ClrType.Name);
            }

            /* SQL dependency işlemindeki OUTPUT ifadesini ortadan kaldırmakta */
            modelBuilder.Entity<Departmanlar>()
                .ToTable("SM_Departmanlar", tb => tb.UseSqlOutputClause(false));

            modelBuilder.Entity<Siralar>()
                .ToTable("SM_Siralar", tb => tb.UseSqlOutputClause(false));
            /* SQL dependency işlemindeki OUTPUT ifadesini ortadan kaldırmakta */

            /* TvBankolari Tablosu için Entitylerde zorunlu düzenlemeler */
            /* Index leme ve multiUnique işlemi */
            modelBuilder.Entity<TvBankolari>()
               .HasIndex(e => new { e.TvId, e.BankoId })
               .IsUnique();

            modelBuilder.Entity<TvBankolari>()
                .HasOne(tb => tb.Tvler)
                .WithMany(t => t.TvBankolari)
                .HasForeignKey(tb => tb.TvId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TvBankolari>()
                .HasOne(tb => tb.Bankolar)
                .WithMany(b => b.TvBankolari)
                .HasForeignKey(tb => tb.BankoId)
                .OnDelete(DeleteBehavior.Restrict);
            /* TvBankolari Tablosu için Entitylerde zorunlu düzenlemeler */

            /* Tvler Tablosu için Entitylerde zorunlu düzenlemeler */
            /* Index leme işlemi */
            modelBuilder.Entity<Tvler>()
               .HasIndex(e => new { e.TvId, e.HizmetBinasiId });
            /* Tvler Tablosu için Entitylerde zorunlu düzenlemeler */

            /* HubTvConnection Tablosu için Entitylerde zorunlu düzenlemeler */
            /* Index leme işlemi */
            modelBuilder.Entity<HubTvConnection>()
               .HasIndex(e => new { e.TvId, e.ConnectionId, e.ConnectionStatus });
            /* Unique işlemi */
            modelBuilder.Entity<HubTvConnection>()
                .HasIndex(e => e.TvId)
                .IsUnique();
            /* HubTvConnection Tablosu için Entitylerde zorunlu düzenlemeler */

            /* HubConnection Tablosu için Entitylerde zorunlu düzenlemeler */
            /* Index leme işlemi */
            modelBuilder.Entity<HubConnection>()
               .HasIndex(e => new { e.TcKimlikNo, e.ConnectionId, e.ConnectionStatus });
            /* Unique işlemi */
            modelBuilder.Entity<HubConnection>()
                .HasIndex(e => e.TcKimlikNo)
                .IsUnique();
            /* HubConnection Tablosu için Entitylerde zorunlu düzenlemeler */

            /* DatabaseLog Tablosu için Entitylerde zorunlu düzenlemeler */
            modelBuilder.Entity<DatabaseLog>()
                .Property(e => e.DatabaseAction)
                .HasConversion<string>();
            /* DatabaseLog Tablosu için Entitylerde zorunlu düzenlemeler */

            /* Sıralar Tablosu için Entitylerde zorunlu düzenlemeler */
            /* MultiUnique işlemi */
            modelBuilder.Entity<Siralar>()
                .HasIndex(s => new { s.SiraNo, s.HizmetBinasiId, s.SiraAlisTarihi })
                .IsUnique();

            modelBuilder.Entity<Siralar>()
                .Property(s => s.BeklemeDurum)
                .HasConversion<int>();
            /* Sıralar Tablosu için Entitylerde zorunlu düzenlemeler */

            /* Personeller Tablosu için Entity lerde zorunlu Tip Düzenlemeleri */
            modelBuilder.Entity<Personeller>()
                .Property(e => e.Cinsiyet)
                .HasConversion<string>();

            modelBuilder.Entity<Personeller>()
                .Property(e => e.PersonelTipi)
                .HasConversion<string>();

            modelBuilder.Entity<Personeller>()
                .Property(e => e.OgrenimDurumu)
                .HasConversion<string>();

            modelBuilder.Entity<Personeller>()
                .Property(e => e.EvDurumu)
                .HasConversion<string>();

            modelBuilder.Entity<Personeller>()
                .Property(e => e.KanGrubu)
                .HasConversion<string>();

            modelBuilder.Entity<Personeller>()
                .Property(e => e.MedeniDurumu)
                .HasConversion<string>();

            modelBuilder.Entity<Personeller>()
                .Property(e => e.EsininIsDurumu)
                .HasConversion<string>();

            modelBuilder.Entity<Personeller>()
                .Property(e => e.SehitYakinligi)
                .HasConversion<string>();

            modelBuilder.Entity<Personeller>()
                .Property(e => e.PersonelAktiflikDurum)
                .HasConversion<int>();

            /* Unique işlemi */
            modelBuilder.Entity<Personeller>()
                .HasIndex(e => e.SicilNo)
                .IsUnique();

            /* Unique işlemi */
            modelBuilder.Entity<Personeller>()
                .HasIndex(e => e.TcKimlikNo)
                .IsUnique();

            modelBuilder.Entity<Personeller>()
                .HasOne(p => p.Il)
                .WithMany()
                .HasForeignKey(p => p.IlId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Personeller>()
                .HasOne(p => p.Ilce)
                .WithMany()
                .HasForeignKey(p => p.IlceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Personeller>()
                .HasOne(p => p.EsininIsIl)
                .WithMany()
                .HasForeignKey(p => p.EsininIsIlId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Personeller>()
                .HasOne(p => p.EsininIsIlce)
                .WithMany()
                .HasForeignKey(p => p.EsininIsIlceId)
                .OnDelete(DeleteBehavior.Restrict);
            /* Personeller Tablosu için Entity lerde zorunlu Tip Düzenlemeleri */

            /* Personel Çocukları için Entity lerde zorunlu Tip Düzenlemesi */
            modelBuilder.Entity<PersonelCocuklari>()
                .Property(e => e.OgrenimDurumu)
                .HasConversion<string>();
            /* Personel Çocukları için Entity lerde zorunlu Tip Düzenlemesi */

            /* Departmanlar Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<Departmanlar>()
                .HasIndex(e => e.DepartmanAdi)
                .IsUnique();

            modelBuilder.Entity<Departmanlar>()
                .Property(e => e.DepartmanAktiflik)
                .HasConversion<int>();
            /* Departmanlar Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* Servisler Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<Servisler>()
                .HasIndex(e => e.ServisAdi)
                .IsUnique();

            modelBuilder.Entity<Servisler>()
                .Property(e => e.ServisAktiflik)
                .HasConversion<int>();
            /* Servisler Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* Unvanlar Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<Unvanlar>()
                .HasIndex(e => e.UnvanAdi)
                .IsUnique();

            modelBuilder.Entity<Unvanlar>()
                .Property(e => e.UnvanAktiflik)
                .HasConversion<int>();
            /* Unvanlar Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* Yetkiler Tablosu için Entitylerde zorunlu Düzenlemeler */
            modelBuilder.Entity<Yetkiler>()
                .Property(e => e.YetkiId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Yetkiler>()
                .Property(e => e.YetkiTuru)
                .HasConversion<string>();
            /* MultiUnique işlemi */
            modelBuilder.Entity<Yetkiler>()
                .HasIndex(e => new { e.UstYetkiId, e.YetkiAdi })
                .IsUnique();
            modelBuilder.Entity<Yetkiler>()
               .HasIndex(e => e.YetkiAdi);
            modelBuilder.Entity<Yetkiler>()
               .HasIndex(e => e.ControllerAdi);
            modelBuilder.Entity<Yetkiler>()
               .HasIndex(e => e.ActionAdi);
            /* Yetkiler Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* Kanallar Tablosu için Entitylerde zorunlu Düzenlemeler */
            modelBuilder.Entity<Kanallar>()
                .HasIndex(e => e.KanalAdi)
                .IsUnique();
            /* Kanallar Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* KanallarAlt Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<KanallarAlt>()
                .HasIndex(e => e.KanalAltAdi)
                .IsUnique();
            /* KanallarAlt Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* KanalPersonelleri Tablosu için Entitylerde zorunlu Düzenlemeler */
            modelBuilder.Entity<KanalPersonelleri>()
                .HasOne(b => b.Personel)
                .WithMany()
                .HasForeignKey(b => b.TcKimlikNo)
                .OnDelete(DeleteBehavior.Restrict);
            /* KanalPersonelleri Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* HizmetBinalari Tablosu için Entitylerde zorunlu Düzenlemeler */
            modelBuilder.Entity<HizmetBinalari>()
                .HasOne(b => b.Departman)
                .WithMany()
                .HasForeignKey(b => b.DepartmanId)
                .OnDelete(DeleteBehavior.Restrict);
            /* HizmetBinalari Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* Bankolar Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* MultiUnique işlemi */
            modelBuilder.Entity<Bankolar>()
                .HasIndex(e => new { e.HizmetBinasiId, e.BankoNo, e.BankoAktiflik })
                .IsUnique();

            modelBuilder.Entity<Bankolar>()
                .Property(e => e.BankoAktiflik)
                .HasConversion<int>();
            /* Bankolar Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* BankolarKullanici Tablosu için Entitylerde zorunlu Düzenlemeler */
            modelBuilder.Entity<BankolarKullanici>()
                .HasOne(b => b.Personel)
                .WithMany()
                .HasForeignKey(b => b.TcKimlikNo)
                .OnDelete(DeleteBehavior.Restrict);

            /* Unique işlemi */
            modelBuilder.Entity<BankolarKullanici>()
                .HasIndex(e => e.BankoId)
                .IsUnique();

            /* Unique işlemi */
            modelBuilder.Entity<BankolarKullanici>()
                .HasIndex(e => e.TcKimlikNo)
                .IsUnique();

            modelBuilder.Entity<BankolarKullanici>()
                .HasKey(bk => bk.BankoKullaniciId);
            /* BankolarKullanici Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* BankoIslemleri Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<BankoIslemleri>()
                .HasIndex(e => e.BankoIslemAdı)
                .IsUnique();

            modelBuilder.Entity<BankoIslemleri>()
                .Property(e => e.BankoIslemAktiflik)
                .HasConversion<int>();

            modelBuilder.Entity<BankoIslemleri>()
                .Property(e => e.BankoGrup)
                .HasConversion<string>();
            /* BankoIslemleri Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* KioskGruplari Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<KioskGruplari>()
                .HasIndex(e => e.KioskGrupAdi)
                .IsUnique();
            /* KioskGruplari Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* KioskIslemGruplari Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* MultiUnique işlemi */
            modelBuilder.Entity<KioskIslemGruplari>()
                .HasIndex(e => new { e.KioskGrupId, e.HizmetBinasiId })
                .IsUnique();
            /* MultiUnique işlemi */
            modelBuilder.Entity<KioskIslemGruplari>()
                .HasIndex(e => new { e.HizmetBinasiId, e.KioskIslemGrupSira })
                .IsUnique();
            modelBuilder.Entity<KioskIslemGruplari>()
                .Property(e => e.KioskIslemGrupAktiflik)
                .HasConversion<int>();
            /* KioskIslemGruplari Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* KanalIslemleri Tablosu için Entitylerde zorunlu Düzenlemeler */
            // Kanallar ve KanalIslemleri arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<KanalIslemleri>()
                .HasOne(ki => ki.Kanallar)
                .WithMany(k => k.KanalIslemleri_)
                .HasForeignKey(ki => ki.KanalId)
                .OnDelete(DeleteBehavior.Restrict);
            // HizmetBinalari ve KanalIslemleri arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<KanalIslemleri>()
                .HasOne(ki => ki.HizmetBinalari)
                .WithMany()
                .HasForeignKey(ki => ki.HizmetBinasiId)
                .OnDelete(DeleteBehavior.Restrict);

            /* MultiUnique işlemi */
            modelBuilder.Entity<KanalIslemleri>()
                .HasIndex(e => new { e.HizmetBinasiId, e.KanalId })
                .IsUnique();

            modelBuilder.Entity<KanalIslemleri>()
                .Property(e => e.KanalIslemAktiflik)
                .HasConversion<int>();
            /* KanalIslemleri Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* KanalAltIslemleri Tablosu için Entitylerde zorunlu Düzenlemeler */
            modelBuilder.Entity<KanalAltIslemleri>()
                .HasIndex(e => new { e.HizmetBinasiId, e.KanalAltId })
                .IsUnique();

            // KanallarAlt ve KanalAltIslemleri arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<KanalAltIslemleri>()
                .HasOne(kai => kai.KanallarAlt)
                .WithMany(ka => ka.KanalAltIslemleri_)
                .HasForeignKey(kai => kai.KanalAltId)
                .OnDelete(DeleteBehavior.Restrict);

            // HizmetBinalari ve KanalAltIslemleri arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<KanalAltIslemleri>()
                .HasOne(kai => kai.HizmetBinalari)
                .WithMany()
                .HasForeignKey(kai => kai.HizmetBinasiId)
                .OnDelete(DeleteBehavior.Restrict);

            // KanalIslemleri ve KanalAltIslemleri arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<KanalAltIslemleri>()
                .HasOne(kai => kai.KanalIslem)
                .WithMany(ki => ki.KanalAltIslemleri_)
                .HasForeignKey(kai => kai.KanalIslemId)
                .OnDelete(DeleteBehavior.Restrict);

            // KioskIslemGruplari ve KanalAltIslemleri arasındaki ilişkiyi yapılandırma
            modelBuilder.Entity<KanalAltIslemleri>()
                .HasOne(kai => kai.KioskIslemGruplari)
                .WithMany()
                .HasForeignKey(kai => kai.KioskIslemGrupId)
                .OnDelete(DeleteBehavior.Restrict);
            /* KanalAltIslemleri Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* KanalPersonelleri Tablosu için Entitylerde zorunlu Düzenlemeler */
            modelBuilder.Entity<KanalPersonelleri>()
                .HasOne(b => b.Personel)
                .WithMany()
                .HasForeignKey(b => b.TcKimlikNo)
                .OnDelete(DeleteBehavior.Restrict);

            /* Unique işlemi */
            modelBuilder.Entity<KanalPersonelleri>()
                .HasIndex(e => new { e.KanalAltIslemId, e.TcKimlikNo })
                .IsUnique();

            modelBuilder.Entity<KanalPersonelleri>()
                .HasKey(bk => bk.KanalPersonelId);
            /* KanalPersonelleri Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* Sendikalar Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<Sendikalar>()
                .HasIndex(e => e.SendikaAdi)
                .IsUnique();
            /* Sendikalar Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* Iller Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<Iller>()
                .HasIndex(e => e.IlAdi)
                .IsUnique();
            /* Iller Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* Ilceler Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Ilceler Tablosu için Entitylerde zorunlu Düzenlemeler */

            /* AtanmaNedenleri Tablosu için Entitylerde zorunlu Düzenlemeler */
            /* Unique işlemi */
            modelBuilder.Entity<AtanmaNedenleri>()
                .HasIndex(e => e.AtanmaNedeni)
                .IsUnique();
            /* AtanmaNedenleri Tablosu için Entitylerde zorunlu Düzenlemeler */
        }
    }
}