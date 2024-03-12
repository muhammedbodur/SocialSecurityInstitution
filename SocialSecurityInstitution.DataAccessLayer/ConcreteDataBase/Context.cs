using Microsoft.EntityFrameworkCore;
using SocialSecurityInstitution.BusinessObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-O8NO0DOE\\SQLEXPRESS2019; initial catalog=SocialSecInstDB;integrated Security=true;TrustServerCertificate=true;");
        }

        public DbSet<Personeller> Personeller { get; set; }
        public DbSet<PersonelCocuklari> PersonelCocuklari { get; set; }
        public DbSet<PersonelYetkileri> PersonelYetkileri { get; set; }
        public DbSet<AtanmaNedenleri> AtanmaNedenleri { get; set; }
        public DbSet<BankoIslemleri> BankoIslemleri { get; set; }
        public DbSet<Bankolar> Bankolar { get; set; }
        public DbSet<BankolarKullanici> BankolarKullanici { get; set; }
        public DbSet<Departmanlar> Departmanlar { get; set; }
        public DbSet<HizmetBinalari> HizmetBinalari { get; set; }
        public DbSet<KanalAltIslemleri> KanalAltIslemleri { get; set; }
        public DbSet<KanalIslemleri> KanalIslemleri { get; set; }
        public DbSet<KanalPersonelleri> KanalPersonelleri { get; set; }
        public DbSet<PdksCihazlar> PdksCihazlar { get; set; }
        public DbSet<Servisler> Servisler { get; set; }
        public DbSet<Unvanlar> Unvanlar { get; set; }
        public DbSet<Sendikalar> Sendikalar { get; set; }
        public DbSet<Yetkiler> Yetkiler { get; set; }
        public DbSet<Iller> Iller { get; set; }
        public DbSet<Ilceler> Ilceler { get; set; }
        public DbSet<LoginLogoutLog> LoginLogoutLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Personeller Tablosu için Entity lerde zorunlu Tip Düzenlemeleri*/
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

            /*Unique işlemi*/
            modelBuilder.Entity<Personeller>()
                .HasIndex(e => e.SicilNo)
                .IsUnique();

            /*Unique işlemi*/
            modelBuilder.Entity<Personeller>()
                .HasIndex(e => e.TcKimlikNo)
                .IsUnique();
            /*Personeller Tablosu için Entity lerde zorunlu Tip Düzenlemeleri*/

            /*Personel Çocukları için Entity lerde zorunlu Tip Düzenlemesi*/
            modelBuilder.Entity<PersonelCocuklari>()
                .Property(e => e.OgrenimDurumu)
                .HasConversion<string>();
            /*Personel Çocukları için Entity lerde zorunlu Tip Düzenlemesi*/

            /*Departmanlar Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<Departmanlar>()
                .HasIndex(e => e.DepartmanAdi)
                .IsUnique();

            modelBuilder.Entity<Departmanlar>()
                .Property(e => e.DepartmanAktiflik)
                .HasConversion<int>();
            /*Departmanlar Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*Servisler Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<Servisler>()
                .HasIndex(e => e.ServisAdi)
                .IsUnique();

            modelBuilder.Entity<Servisler>()
                .Property(e => e.ServisAktiflik)
                .HasConversion<int>();
            /*Servisler Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*Unvanlar Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<Unvanlar>()
                .HasIndex(e => e.UnvanAdi)
                .IsUnique();

            modelBuilder.Entity<Unvanlar>()
                .Property(e => e.UnvanAktiflik)
                .HasConversion<int>();
            /*Unvanlar Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*Yetkiler Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<Yetkiler>()
                .HasIndex(e => new { e.YetkiAdi, e.YetkiAktiflik })
                .IsUnique();

            modelBuilder.Entity<Yetkiler>()
                .Property(e => e.YetkiTuru)
                .HasConversion<string>();

            modelBuilder.Entity<Yetkiler>()
                .Property(e => e.YetkiAktiflik)
                .HasConversion<int>();
            /*Yetkiler Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*PersonelYetkileri Tablosu için Entitylerde zorunlu Düzenlemeler*/
            modelBuilder.Entity<PersonelYetkileri>()
                .Property(e => e.YetkiTipi)
                .HasConversion<string>();
            /*PersonelYetkileri Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*Bankolar Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*MultiUnique işlemi*/
            modelBuilder.Entity<Bankolar>()
                .HasIndex(e => new { e.DepartmanId, e.BankoNo , e.BankoAktiflik })
                .IsUnique();

            modelBuilder.Entity<Bankolar>()
                .Property(e => e.BankoAktiflik)
                .HasConversion<int>();
            /*Bankolar Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*BankolarKullanici Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<BankolarKullanici>()
                .HasIndex(e => e.BankoId)
                .IsUnique();

            /*Unique işlemi*/
            modelBuilder.Entity<BankolarKullanici>()
                .HasIndex(e => e.TcKimlikNo)
                .IsUnique();

            modelBuilder.Entity<BankolarKullanici>()
                .HasKey(bk => bk.Id);
            /*BankolarKullanici Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*BankoIslemleri Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<BankoIslemleri>()
                .HasIndex(e => e.BankoIslemAdı)
                .IsUnique();

            modelBuilder.Entity<BankoIslemleri>()
                .Property(e => e.BankoIslemAktiflik)
                .HasConversion<int>();

            modelBuilder.Entity<BankoIslemleri>()
                .Property(e => e.BankoGrup)
                .HasConversion<string>();
            /*BankoIslemleri Tablosu için Entitylerde zorunlu Düzenlemeler*/
            
            /*Sendikalar Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<Sendikalar>()
                .HasIndex(e => e.SendikaAdi)
                .IsUnique();
            /*Sendikalar Tablosu için Entitylerde zorunlu Düzenlemeler*/
            
            /*Iller Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<Iller>()
                .HasIndex(e => e.IlAdi)
                .IsUnique();
            /*Iller Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*Ilceler Tablosu için Entitylerde zorunlu Düzenlemeler*/
            
            /*Ilceler Tablosu için Entitylerde zorunlu Düzenlemeler*/

            /*AtanmaNedenleri Tablosu için Entitylerde zorunlu Düzenlemeler*/
            /*Unique işlemi*/
            modelBuilder.Entity<AtanmaNedenleri>()
                .HasIndex(e => e.AtanmaNedeni)
                .IsUnique();
            /*Ilceler Tablosu için Entitylerde zorunlu Düzenlemeler*/
        }
    }

}
