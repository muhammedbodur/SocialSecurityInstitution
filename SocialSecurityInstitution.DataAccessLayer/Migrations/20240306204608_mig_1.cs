using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialSecurityInstitution.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtanmaNedenleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtanmaNedeni = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtanmaNedenleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankoIslemleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankoGrup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankoUstIslemId = table.Column<int>(type: "int", nullable: false),
                    BankoIslemAdı = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankoIslemSira = table.Column<int>(type: "int", nullable: false),
                    BankoIslemAktiflik = table.Column<int>(type: "int", nullable: false),
                    DiffLang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankoIslemleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departmanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmanAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmanlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HizmetBinalari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetBinasi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizmetBinalari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Iller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlAdi = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KanalIslemleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanalIslemAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaslangicNumara = table.Column<int>(type: "int", nullable: false),
                    BitisNumara = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanalIslemleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sendikalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendikaAdi = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sendikalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServisAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServisAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servisler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unvanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnvanAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnvanAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unvanlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yetkiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YetkiTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YetkiAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UstYetkiId = table.Column<int>(type: "int", nullable: false),
                    YetkiAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yetkiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bankolar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    BankoNo = table.Column<int>(type: "int", nullable: false),
                    BankoAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bankolar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bankolar_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PdksCihazlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    CihazIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CihazPort = table.Column<int>(type: "int", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktiflik = table.Column<int>(type: "int", nullable: false),
                    IslemZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IslemSayisi = table.Column<int>(type: "int", nullable: false),
                    IslemBasari = table.Column<int>(type: "int", nullable: false),
                    IslemDurum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontrolZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KontrolSayisi = table.Column<int>(type: "int", nullable: false),
                    KontrolBasari = table.Column<int>(type: "int", nullable: false),
                    KontrolDurum = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdksCihazlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PdksCihazlar_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ilceler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilceler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ilceler_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanalAltIslemleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanalIslemId = table.Column<int>(type: "int", nullable: false),
                    KanalAltIslemAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanalAltIslemleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanalAltIslemleri_KanalIslemleri_KanalIslemId",
                        column: x => x.KanalIslemId,
                        principalTable: "KanalIslemleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SicilNo = table.Column<int>(type: "int", nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    ServisId = table.Column<int>(type: "int", nullable: false),
                    UnvanId = table.Column<int>(type: "int", nullable: false),
                    Gorev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uzmanlik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtanmaNedeniId = table.Column<int>(type: "int", nullable: true),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: true),
                    PersonelTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dahili = table.Column<int>(type: "int", nullable: false),
                    CepTelefonu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CepTelefonu2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvTelefonu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlId = table.Column<int>(type: "int", nullable: true),
                    IlceId = table.Column<int>(type: "int", nullable: true),
                    Semt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedeniDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KanGrubu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UlasimServis1 = table.Column<int>(type: "int", nullable: false),
                    UlasimServis2 = table.Column<int>(type: "int", nullable: false),
                    Tabldot = table.Column<int>(type: "int", nullable: false),
                    PersonelAktiflikDurum = table.Column<int>(type: "int", nullable: false),
                    EmekliSicilNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OgrenimDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BitirdigiOkul = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BitirdigiBolum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OgrenimSuresi = table.Column<int>(type: "int", nullable: false),
                    Bransi = table.Column<int>(type: "int", nullable: false),
                    SendikaId = table.Column<int>(type: "int", nullable: true),
                    SehitYakinligi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsininAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsininIsDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsininUnvani = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsininIsAdresi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsininIsIlId = table.Column<int>(type: "int", nullable: true),
                    EsininIsIlceId = table.Column<int>(type: "int", nullable: true),
                    EsininIsSemt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CocukBilgileri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HizmetBilgisi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EgitimBilgisi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImzaYetkileri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CezaBilgileri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngelBilgileri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.TcKimlikNo);
                    table.ForeignKey(
                        name: "FK_Personeller_AtanmaNedenleri_AtanmaNedeniId",
                        column: x => x.AtanmaNedeniId,
                        principalTable: "AtanmaNedenleri",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personeller_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personeller_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "HizmetBinalari",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personeller_Ilceler_EsininIsIlceId",
                        column: x => x.EsininIsIlceId,
                        principalTable: "Ilceler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personeller_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilceler",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personeller_Iller_EsininIsIlId",
                        column: x => x.EsininIsIlId,
                        principalTable: "Iller",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personeller_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personeller_Sendikalar_SendikaId",
                        column: x => x.SendikaId,
                        principalTable: "Sendikalar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personeller_Servisler_ServisId",
                        column: x => x.ServisId,
                        principalTable: "Servisler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personeller_Unvanlar_UnvanId",
                        column: x => x.UnvanId,
                        principalTable: "Unvanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankolarKullanici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankoId = table.Column<int>(type: "int", nullable: false),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonellerTcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankolarKullanici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankolarKullanici_Bankolar_BankoId",
                        column: x => x.BankoId,
                        principalTable: "Bankolar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankolarKullanici_Personeller_PersonellerTcKimlikNo",
                        column: x => x.PersonellerTcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo");
                });

            migrationBuilder.CreateTable(
                name: "KanalPersonelleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KanalAltIslemId = table.Column<int>(type: "int", nullable: false),
                    PersonelKanalAltIslemAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanalPersonelleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanalPersonelleri_KanalAltIslemleri_KanalAltIslemId",
                        column: x => x.KanalAltIslemId,
                        principalTable: "KanalAltIslemleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KanalPersonelleri_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogoutLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogoutLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginLogoutLog_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelCocuklari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CocukAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CocukDogumTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    OgrenimDurumu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelCocuklari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelCocuklari_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelYetkileri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YetkiId = table.Column<int>(type: "int", nullable: false),
                    YetkiTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelYetkileri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelYetkileri_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelYetkileri_Yetkiler_YetkiId",
                        column: x => x.YetkiId,
                        principalTable: "Yetkiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtanmaNedenleri_AtanmaNedeni",
                table: "AtanmaNedenleri",
                column: "AtanmaNedeni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankoIslemleri_BankoIslemAdı",
                table: "BankoIslemleri",
                column: "BankoIslemAdı",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bankolar_DepartmanId_BankoNo_BankoAktiflik",
                table: "Bankolar",
                columns: new[] { "DepartmanId", "BankoNo", "BankoAktiflik" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankolarKullanici_BankoId",
                table: "BankolarKullanici",
                column: "BankoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankolarKullanici_PersonellerTcKimlikNo",
                table: "BankolarKullanici",
                column: "PersonellerTcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_BankolarKullanici_TcKimlikNo",
                table: "BankolarKullanici",
                column: "TcKimlikNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departmanlar_DepartmanAdi",
                table: "Departmanlar",
                column: "DepartmanAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ilceler_IlId",
                table: "Ilceler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Iller_IlAdi",
                table: "Iller",
                column: "IlAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanalAltIslemleri_KanalIslemId",
                table: "KanalAltIslemleri",
                column: "KanalIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_KanalPersonelleri_KanalAltIslemId",
                table: "KanalPersonelleri",
                column: "KanalAltIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_KanalPersonelleri_TcKimlikNo",
                table: "KanalPersonelleri",
                column: "TcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_LoginLogoutLog_TcKimlikNo",
                table: "LoginLogoutLog",
                column: "TcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_PdksCihazlar_DepartmanId",
                table: "PdksCihazlar",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelCocuklari_TcKimlikNo",
                table: "PersonelCocuklari",
                column: "TcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_AtanmaNedeniId",
                table: "Personeller",
                column: "AtanmaNedeniId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_DepartmanId",
                table: "Personeller",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_EsininIsIlceId",
                table: "Personeller",
                column: "EsininIsIlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_EsininIsIlId",
                table: "Personeller",
                column: "EsininIsIlId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_HizmetBinasiId",
                table: "Personeller",
                column: "HizmetBinasiId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_IlceId",
                table: "Personeller",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_IlId",
                table: "Personeller",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_SendikaId",
                table: "Personeller",
                column: "SendikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_ServisId",
                table: "Personeller",
                column: "ServisId");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_SicilNo",
                table: "Personeller",
                column: "SicilNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_TcKimlikNo",
                table: "Personeller",
                column: "TcKimlikNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_UnvanId",
                table: "Personeller",
                column: "UnvanId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelYetkileri_TcKimlikNo",
                table: "PersonelYetkileri",
                column: "TcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelYetkileri_YetkiId",
                table: "PersonelYetkileri",
                column: "YetkiId");

            migrationBuilder.CreateIndex(
                name: "IX_Sendikalar_SendikaAdi",
                table: "Sendikalar",
                column: "SendikaAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servisler_ServisAdi",
                table: "Servisler",
                column: "ServisAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unvanlar_UnvanAdi",
                table: "Unvanlar",
                column: "UnvanAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Yetkiler_YetkiAdi_YetkiAktiflik",
                table: "Yetkiler",
                columns: new[] { "YetkiAdi", "YetkiAktiflik" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankoIslemleri");

            migrationBuilder.DropTable(
                name: "BankolarKullanici");

            migrationBuilder.DropTable(
                name: "KanalPersonelleri");

            migrationBuilder.DropTable(
                name: "LoginLogoutLog");

            migrationBuilder.DropTable(
                name: "PdksCihazlar");

            migrationBuilder.DropTable(
                name: "PersonelCocuklari");

            migrationBuilder.DropTable(
                name: "PersonelYetkileri");

            migrationBuilder.DropTable(
                name: "Bankolar");

            migrationBuilder.DropTable(
                name: "KanalAltIslemleri");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "Yetkiler");

            migrationBuilder.DropTable(
                name: "KanalIslemleri");

            migrationBuilder.DropTable(
                name: "AtanmaNedenleri");

            migrationBuilder.DropTable(
                name: "Departmanlar");

            migrationBuilder.DropTable(
                name: "HizmetBinalari");

            migrationBuilder.DropTable(
                name: "Ilceler");

            migrationBuilder.DropTable(
                name: "Sendikalar");

            migrationBuilder.DropTable(
                name: "Servisler");

            migrationBuilder.DropTable(
                name: "Unvanlar");

            migrationBuilder.DropTable(
                name: "Iller");
        }
    }
}
