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
                    AtanmaNedeniId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtanmaNedeni = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtanmaNedenleri", x => x.AtanmaNedeniId);
                });

            migrationBuilder.CreateTable(
                name: "BankoIslemleri",
                columns: table => new
                {
                    BankoIslemId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_BankoIslemleri", x => x.BankoIslemId);
                });

            migrationBuilder.CreateTable(
                name: "Departmanlar",
                columns: table => new
                {
                    DepartmanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmanAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departmanlar", x => x.DepartmanId);
                });

            migrationBuilder.CreateTable(
                name: "Iller",
                columns: table => new
                {
                    IlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlAdi = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iller", x => x.IlId);
                });

            migrationBuilder.CreateTable(
                name: "Kanallar",
                columns: table => new
                {
                    KanalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanalAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanallar", x => x.KanalId);
                });

            migrationBuilder.CreateTable(
                name: "KioskIslemGruplari",
                columns: table => new
                {
                    KioskIslemGrupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KioskIslemGrupAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KioskIslemGrupSira = table.Column<int>(type: "int", nullable: false),
                    KioskIslemGrupAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KioskIslemGruplari", x => x.KioskIslemGrupId);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogoutLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogoutLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modul",
                columns: table => new
                {
                    ModulId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModulAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modul", x => x.ModulId);
                });

            migrationBuilder.CreateTable(
                name: "PersonelCocuklari",
                columns: table => new
                {
                    PersonelCocukId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelTcKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CocukAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CocukDogumTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    OgrenimDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelCocuklari", x => x.PersonelCocukId);
                });

            migrationBuilder.CreateTable(
                name: "Sendikalar",
                columns: table => new
                {
                    SendikaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendikaAdi = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sendikalar", x => x.SendikaId);
                });

            migrationBuilder.CreateTable(
                name: "Servisler",
                columns: table => new
                {
                    ServisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServisAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServisAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servisler", x => x.ServisId);
                });

            migrationBuilder.CreateTable(
                name: "Unvanlar",
                columns: table => new
                {
                    UnvanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnvanAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnvanAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unvanlar", x => x.UnvanId);
                });

            migrationBuilder.CreateTable(
                name: "Yetkiler",
                columns: table => new
                {
                    YetkiId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Yetkiler", x => x.YetkiId);
                });

            migrationBuilder.CreateTable(
                name: "HizmetBinalari",
                columns: table => new
                {
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetBinasiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    HizmetBinasiAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmanlarDepartmanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizmetBinalari", x => x.HizmetBinasiId);
                    table.ForeignKey(
                        name: "FK_HizmetBinalari_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "DepartmanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HizmetBinalari_Departmanlar_DepartmanlarDepartmanId",
                        column: x => x.DepartmanlarDepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "DepartmanId");
                });

            migrationBuilder.CreateTable(
                name: "PdksCihazlar",
                columns: table => new
                {
                    PdksCihazId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_PdksCihazlar", x => x.PdksCihazId);
                    table.ForeignKey(
                        name: "FK_PdksCihazlar_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "DepartmanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ilceler",
                columns: table => new
                {
                    IlceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilceler", x => x.IlceId);
                    table.ForeignKey(
                        name: "FK_Ilceler_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanallarAlt",
                columns: table => new
                {
                    KanalAltId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanalAltAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KanallarKanalId = table.Column<int>(type: "int", nullable: true),
                    KioskIslemGruplariKioskIslemGrupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanallarAlt", x => x.KanalAltId);
                    table.ForeignKey(
                        name: "FK_KanallarAlt_Kanallar_KanallarKanalId",
                        column: x => x.KanallarKanalId,
                        principalTable: "Kanallar",
                        principalColumn: "KanalId");
                    table.ForeignKey(
                        name: "FK_KanallarAlt_KioskIslemGruplari_KioskIslemGruplariKioskIslemGrupId",
                        column: x => x.KioskIslemGruplariKioskIslemGrupId,
                        principalTable: "KioskIslemGruplari",
                        principalColumn: "KioskIslemGrupId");
                });

            migrationBuilder.CreateTable(
                name: "ModulController",
                columns: table => new
                {
                    ControllerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControllerAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModulId = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulController", x => x.ControllerId);
                    table.ForeignKey(
                        name: "FK_ModulController_Modul_ModulId",
                        column: x => x.ModulId,
                        principalTable: "Modul",
                        principalColumn: "ModulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModullerAlt",
                columns: table => new
                {
                    ModulAltId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModulAltAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModulId = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModullerAlt", x => x.ModulAltId);
                    table.ForeignKey(
                        name: "FK_ModullerAlt_Modul_ModulId",
                        column: x => x.ModulId,
                        principalTable: "Modul",
                        principalColumn: "ModulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelYetkileriii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelTcKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YetkiId = table.Column<int>(type: "int", nullable: false),
                    YetkiTipi = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelYetkileriii", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelYetkileriii_Yetkiler_YetkiId",
                        column: x => x.YetkiId,
                        principalTable: "Yetkiler",
                        principalColumn: "YetkiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bankolar",
                columns: table => new
                {
                    BankoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    BankoNo = table.Column<int>(type: "int", nullable: false),
                    BankoAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bankolar", x => x.BankoId);
                    table.ForeignKey(
                        name: "FK_Bankolar_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanalIslemleri",
                columns: table => new
                {
                    KanalIslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanalId = table.Column<int>(type: "int", nullable: false),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    BaslangicNumara = table.Column<int>(type: "int", nullable: false),
                    BitisNumara = table.Column<int>(type: "int", nullable: false),
                    KanalIslemAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanalIslemleri", x => x.KanalIslemId);
                    table.ForeignKey(
                        name: "FK_KanalIslemleri_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KanalIslemleri_Kanallar_KanalId",
                        column: x => x.KanalId,
                        principalTable: "Kanallar",
                        principalColumn: "KanalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SicilNo = table.Column<int>(type: "int", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonelKayitNo = table.Column<int>(type: "int", nullable: false),
                    KartNo = table.Column<int>(type: "int", nullable: false),
                    KartNoAktiflikTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KartNoDuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KartNoGonderimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KartGonderimIslemBasari = table.Column<int>(type: "int", nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    ServisId = table.Column<int>(type: "int", nullable: false),
                    UnvanId = table.Column<int>(type: "int", nullable: false),
                    Gorev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uzmanlik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtanmaNedeniId = table.Column<int>(type: "int", nullable: false),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    PersonelTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dahili = table.Column<int>(type: "int", nullable: false),
                    CepTelefonu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CepTelefonu2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvTelefonu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    IlceId = table.Column<int>(type: "int", nullable: false),
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
                    Bransi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendikaId = table.Column<int>(type: "int", nullable: false),
                    SehitYakinligi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsininAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsininIsDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsininUnvani = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsininIsAdresi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsininIsIlId = table.Column<int>(type: "int", nullable: false),
                    EsininIsIlceId = table.Column<int>(type: "int", nullable: false),
                    EsininIsSemt = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        principalColumn: "AtanmaNedeniId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personeller_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departmanlar",
                        principalColumn: "DepartmanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personeller_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personeller_Ilceler_EsininIsIlceId",
                        column: x => x.EsininIsIlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personeller_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personeller_Iller_EsininIsIlId",
                        column: x => x.EsininIsIlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personeller_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personeller_Sendikalar_SendikaId",
                        column: x => x.SendikaId,
                        principalTable: "Sendikalar",
                        principalColumn: "SendikaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personeller_Servisler_ServisId",
                        column: x => x.ServisId,
                        principalTable: "Servisler",
                        principalColumn: "ServisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personeller_Unvanlar_UnvanId",
                        column: x => x.UnvanId,
                        principalTable: "Unvanlar",
                        principalColumn: "UnvanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModulControllerIslemler",
                columns: table => new
                {
                    ControllerIslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControllerIslemAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControllerId = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulControllerIslemler", x => x.ControllerIslemId);
                    table.ForeignKey(
                        name: "FK_ModulControllerIslemler_ModulController_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "ModulController",
                        principalColumn: "ControllerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanalAltIslemleri",
                columns: table => new
                {
                    KanalAltIslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanalAltId = table.Column<int>(type: "int", nullable: false),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    KanalIslemId = table.Column<int>(type: "int", nullable: false),
                    KioskIslemGrupId = table.Column<int>(type: "int", nullable: true),
                    KanalAltIslemAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanalAltIslemleri", x => x.KanalAltIslemId);
                    table.ForeignKey(
                        name: "FK_KanalAltIslemleri_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KanalAltIslemleri_KanalIslemleri_KanalIslemId",
                        column: x => x.KanalIslemId,
                        principalTable: "KanalIslemleri",
                        principalColumn: "KanalIslemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KanalAltIslemleri_KanallarAlt_KanalAltId",
                        column: x => x.KanalAltId,
                        principalTable: "KanallarAlt",
                        principalColumn: "KanalAltId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KanalAltIslemleri_KioskIslemGruplari_KioskIslemGrupId",
                        column: x => x.KioskIslemGrupId,
                        principalTable: "KioskIslemGruplari",
                        principalColumn: "KioskIslemGrupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankolarKullanici",
                columns: table => new
                {
                    BankoKullaniciId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankoId = table.Column<int>(type: "int", nullable: false),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonellerTcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankolarKullanici", x => x.BankoKullaniciId);
                    table.ForeignKey(
                        name: "FK_BankolarKullanici_Bankolar_BankoId",
                        column: x => x.BankoId,
                        principalTable: "Bankolar",
                        principalColumn: "BankoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankolarKullanici_Personeller_PersonellerTcKimlikNo",
                        column: x => x.PersonellerTcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo");
                    table.ForeignKey(
                        name: "FK_BankolarKullanici_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonelYetkileri",
                columns: table => new
                {
                    PersonelYetkiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YetkiControllerIslemId = table.Column<int>(type: "int", nullable: false),
                    YetkiTipleri = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelYetkileri", x => x.PersonelYetkiId);
                    table.ForeignKey(
                        name: "FK_PersonelYetkileri_ModulControllerIslemler_YetkiControllerIslemId",
                        column: x => x.YetkiControllerIslemId,
                        principalTable: "ModulControllerIslemler",
                        principalColumn: "ControllerIslemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelYetkileri_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanalPersonelleri",
                columns: table => new
                {
                    KanalPersonelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KanalAltIslemId = table.Column<int>(type: "int", nullable: false),
                    KanalAltIslemPersonelAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonellerTcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanalPersonelleri", x => x.KanalPersonelId);
                    table.ForeignKey(
                        name: "FK_KanalPersonelleri_KanalAltIslemleri_KanalAltIslemId",
                        column: x => x.KanalAltIslemId,
                        principalTable: "KanalAltIslemleri",
                        principalColumn: "KanalAltIslemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KanalPersonelleri_Personeller_PersonellerTcKimlikNo",
                        column: x => x.PersonellerTcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo");
                    table.ForeignKey(
                        name: "FK_KanalPersonelleri_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Bankolar_HizmetBinasiId_BankoNo_BankoAktiflik",
                table: "Bankolar",
                columns: new[] { "HizmetBinasiId", "BankoNo", "BankoAktiflik" },
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
                name: "IX_HizmetBinalari_DepartmanId",
                table: "HizmetBinalari",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_HizmetBinalari_DepartmanlarDepartmanId",
                table: "HizmetBinalari",
                column: "DepartmanlarDepartmanId");

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
                name: "IX_KanalAltIslemleri_HizmetBinasiId",
                table: "KanalAltIslemleri",
                column: "HizmetBinasiId");

            migrationBuilder.CreateIndex(
                name: "IX_KanalAltIslemleri_KanalAltId",
                table: "KanalAltIslemleri",
                column: "KanalAltId");

            migrationBuilder.CreateIndex(
                name: "IX_KanalAltIslemleri_KanalIslemId",
                table: "KanalAltIslemleri",
                column: "KanalIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_KanalAltIslemleri_KioskIslemGrupId",
                table: "KanalAltIslemleri",
                column: "KioskIslemGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_KanalIslemleri_HizmetBinasiId_KanalId",
                table: "KanalIslemleri",
                columns: new[] { "HizmetBinasiId", "KanalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanalIslemleri_KanalId",
                table: "KanalIslemleri",
                column: "KanalId");

            migrationBuilder.CreateIndex(
                name: "IX_Kanallar_KanalAdi",
                table: "Kanallar",
                column: "KanalAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanallarAlt_KanalAltAdi",
                table: "KanallarAlt",
                column: "KanalAltAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanallarAlt_KanallarKanalId",
                table: "KanallarAlt",
                column: "KanallarKanalId");

            migrationBuilder.CreateIndex(
                name: "IX_KanallarAlt_KioskIslemGruplariKioskIslemGrupId",
                table: "KanallarAlt",
                column: "KioskIslemGruplariKioskIslemGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_KanalPersonelleri_KanalAltIslemId_TcKimlikNo",
                table: "KanalPersonelleri",
                columns: new[] { "KanalAltIslemId", "TcKimlikNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanalPersonelleri_PersonellerTcKimlikNo",
                table: "KanalPersonelleri",
                column: "PersonellerTcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_KanalPersonelleri_TcKimlikNo",
                table: "KanalPersonelleri",
                column: "TcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_KioskIslemGruplari_KioskIslemGrupAdi_KioskIslemGrupAktiflik",
                table: "KioskIslemGruplari",
                columns: new[] { "KioskIslemGrupAdi", "KioskIslemGrupAktiflik" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModulController_ModulId",
                table: "ModulController",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_ModulControllerIslemler_ControllerId",
                table: "ModulControllerIslemler",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_ModullerAlt_ModulId",
                table: "ModullerAlt",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_PdksCihazlar_DepartmanId",
                table: "PdksCihazlar",
                column: "DepartmanId");

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
                name: "IX_PersonelYetkileri_YetkiControllerIslemId",
                table: "PersonelYetkileri",
                column: "YetkiControllerIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelYetkileriii_YetkiId",
                table: "PersonelYetkileriii",
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
                name: "ModullerAlt");

            migrationBuilder.DropTable(
                name: "PdksCihazlar");

            migrationBuilder.DropTable(
                name: "PersonelCocuklari");

            migrationBuilder.DropTable(
                name: "PersonelYetkileri");

            migrationBuilder.DropTable(
                name: "PersonelYetkileriii");

            migrationBuilder.DropTable(
                name: "Bankolar");

            migrationBuilder.DropTable(
                name: "KanalAltIslemleri");

            migrationBuilder.DropTable(
                name: "ModulControllerIslemler");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "Yetkiler");

            migrationBuilder.DropTable(
                name: "KanalIslemleri");

            migrationBuilder.DropTable(
                name: "KanallarAlt");

            migrationBuilder.DropTable(
                name: "ModulController");

            migrationBuilder.DropTable(
                name: "AtanmaNedenleri");

            migrationBuilder.DropTable(
                name: "Ilceler");

            migrationBuilder.DropTable(
                name: "Sendikalar");

            migrationBuilder.DropTable(
                name: "Servisler");

            migrationBuilder.DropTable(
                name: "Unvanlar");

            migrationBuilder.DropTable(
                name: "HizmetBinalari");

            migrationBuilder.DropTable(
                name: "Kanallar");

            migrationBuilder.DropTable(
                name: "KioskIslemGruplari");

            migrationBuilder.DropTable(
                name: "Modul");

            migrationBuilder.DropTable(
                name: "Iller");

            migrationBuilder.DropTable(
                name: "Departmanlar");
        }
    }
}
