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
                name: "SM_AtanmaNedenleri",
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
                    table.PrimaryKey("PK_SM_AtanmaNedenleri", x => x.AtanmaNedeniId);
                });

            migrationBuilder.CreateTable(
                name: "SM_BankoIslemleri",
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
                    table.PrimaryKey("PK_SM_BankoIslemleri", x => x.BankoIslemId);
                });

            migrationBuilder.CreateTable(
                name: "SM_Departmanlar",
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
                    table.PrimaryKey("PK_SM_Departmanlar", x => x.DepartmanId);
                });

            migrationBuilder.CreateTable(
                name: "SM_Iller",
                columns: table => new
                {
                    IlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlAdi = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_Iller", x => x.IlId);
                });

            migrationBuilder.CreateTable(
                name: "SM_Kanallar",
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
                    table.PrimaryKey("PK_SM_Kanallar", x => x.KanalId);
                });

            migrationBuilder.CreateTable(
                name: "SM_KioskGruplari",
                columns: table => new
                {
                    KioskGrupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KioskGrupAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_KioskGruplari", x => x.KioskGrupId);
                });

            migrationBuilder.CreateTable(
                name: "SM_LoginLogoutLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SessionID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_LoginLogoutLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SM_Moduller",
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
                    table.PrimaryKey("PK_SM_Moduller", x => x.ModulId);
                });

            migrationBuilder.CreateTable(
                name: "SM_PersonelCocuklari",
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
                    table.PrimaryKey("PK_SM_PersonelCocuklari", x => x.PersonelCocukId);
                });

            migrationBuilder.CreateTable(
                name: "SM_Sendikalar",
                columns: table => new
                {
                    SendikaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendikaAdi = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_Sendikalar", x => x.SendikaId);
                });

            migrationBuilder.CreateTable(
                name: "SM_Servisler",
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
                    table.PrimaryKey("PK_SM_Servisler", x => x.ServisId);
                });

            migrationBuilder.CreateTable(
                name: "SM_Unvanlar",
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
                    table.PrimaryKey("PK_SM_Unvanlar", x => x.UnvanId);
                });

            migrationBuilder.CreateTable(
                name: "SM_Yetkiler",
                columns: table => new
                {
                    YetkiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YetkiTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YetkiAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UstYetkiId = table.Column<int>(type: "int", nullable: true),
                    ControllerAdi = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActionAdi = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_Yetkiler", x => x.YetkiId);
                });

            migrationBuilder.CreateTable(
                name: "SM_HizmetBinalari",
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
                    table.PrimaryKey("PK_SM_HizmetBinalari", x => x.HizmetBinasiId);
                    table.ForeignKey(
                        name: "FK_SM_HizmetBinalari_SM_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "SM_Departmanlar",
                        principalColumn: "DepartmanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_HizmetBinalari_SM_Departmanlar_DepartmanlarDepartmanId",
                        column: x => x.DepartmanlarDepartmanId,
                        principalTable: "SM_Departmanlar",
                        principalColumn: "DepartmanId");
                });

            migrationBuilder.CreateTable(
                name: "SM_PdksCihazlar",
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
                    table.PrimaryKey("PK_SM_PdksCihazlar", x => x.PdksCihazId);
                    table.ForeignKey(
                        name: "FK_SM_PdksCihazlar_SM_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "SM_Departmanlar",
                        principalColumn: "DepartmanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_Ilceler",
                columns: table => new
                {
                    IlceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_Ilceler", x => x.IlceId);
                    table.ForeignKey(
                        name: "FK_SM_Ilceler_SM_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "SM_Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_ModulController",
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
                    table.PrimaryKey("PK_SM_ModulController", x => x.ControllerId);
                    table.ForeignKey(
                        name: "FK_SM_ModulController_SM_Moduller_ModulId",
                        column: x => x.ModulId,
                        principalTable: "SM_Moduller",
                        principalColumn: "ModulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_ModullerAlt",
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
                    table.PrimaryKey("PK_SM_ModullerAlt", x => x.ModulAltId);
                    table.ForeignKey(
                        name: "FK_SM_ModullerAlt_SM_Moduller_ModulId",
                        column: x => x.ModulId,
                        principalTable: "SM_Moduller",
                        principalColumn: "ModulId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_Bankolar",
                columns: table => new
                {
                    BankoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    BankoNo = table.Column<int>(type: "int", nullable: false),
                    BankoTipi = table.Column<int>(type: "int", nullable: false),
                    KatTipi = table.Column<int>(type: "int", nullable: false),
                    BankoAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_Bankolar", x => x.BankoId);
                    table.ForeignKey(
                        name: "FK_SM_Bankolar_SM_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "SM_HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_KanalIslemleri",
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
                    table.PrimaryKey("PK_SM_KanalIslemleri", x => x.KanalIslemId);
                    table.ForeignKey(
                        name: "FK_SM_KanalIslemleri_SM_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "SM_HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_KanalIslemleri_SM_Kanallar_KanalId",
                        column: x => x.KanalId,
                        principalTable: "SM_Kanallar",
                        principalColumn: "KanalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SM_KioskIslemGruplari",
                columns: table => new
                {
                    KioskIslemGrupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KioskGrupId = table.Column<int>(type: "int", nullable: false),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    KioskIslemGrupSira = table.Column<int>(type: "int", nullable: false),
                    KioskIslemGrupAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_KioskIslemGruplari", x => x.KioskIslemGrupId);
                    table.ForeignKey(
                        name: "FK_SM_KioskIslemGruplari_SM_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "SM_HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_KioskIslemGruplari_SM_KioskGruplari_KioskGrupId",
                        column: x => x.KioskGrupId,
                        principalTable: "SM_KioskGruplari",
                        principalColumn: "KioskGrupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_Tvler",
                columns: table => new
                {
                    TvId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    KatTipi = table.Column<int>(type: "int", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IslemZamani = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_Tvler", x => x.TvId);
                    table.ForeignKey(
                        name: "FK_SM_Tvler_SM_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "SM_HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_Personeller",
                columns: table => new
                {
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SicilNo = table.Column<int>(type: "int", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonelKayitNo = table.Column<int>(type: "int", nullable: false),
                    KartNo = table.Column<int>(type: "int", nullable: false),
                    KartNoAktiflikTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KartNoDuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KartNoGonderimTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_Personeller", x => x.TcKimlikNo);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_AtanmaNedenleri_AtanmaNedeniId",
                        column: x => x.AtanmaNedeniId,
                        principalTable: "SM_AtanmaNedenleri",
                        principalColumn: "AtanmaNedeniId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_Departmanlar_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "SM_Departmanlar",
                        principalColumn: "DepartmanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "SM_HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_Ilceler_EsininIsIlceId",
                        column: x => x.EsininIsIlceId,
                        principalTable: "SM_Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "SM_Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_Iller_EsininIsIlId",
                        column: x => x.EsininIsIlId,
                        principalTable: "SM_Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "SM_Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_Sendikalar_SendikaId",
                        column: x => x.SendikaId,
                        principalTable: "SM_Sendikalar",
                        principalColumn: "SendikaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_Servisler_ServisId",
                        column: x => x.ServisId,
                        principalTable: "SM_Servisler",
                        principalColumn: "ServisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_Personeller_SM_Unvanlar_UnvanId",
                        column: x => x.UnvanId,
                        principalTable: "SM_Unvanlar",
                        principalColumn: "UnvanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_ModulControllerIslemler",
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
                    table.PrimaryKey("PK_SM_ModulControllerIslemler", x => x.ControllerIslemId);
                    table.ForeignKey(
                        name: "FK_SM_ModulControllerIslemler_SM_ModulController_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "SM_ModulController",
                        principalColumn: "ControllerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_KanallarAlt",
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
                    table.PrimaryKey("PK_SM_KanallarAlt", x => x.KanalAltId);
                    table.ForeignKey(
                        name: "FK_SM_KanallarAlt_SM_Kanallar_KanallarKanalId",
                        column: x => x.KanallarKanalId,
                        principalTable: "SM_Kanallar",
                        principalColumn: "KanalId");
                    table.ForeignKey(
                        name: "FK_SM_KanallarAlt_SM_KioskIslemGruplari_KioskIslemGruplariKioskIslemGrupId",
                        column: x => x.KioskIslemGruplariKioskIslemGrupId,
                        principalTable: "SM_KioskIslemGruplari",
                        principalColumn: "KioskIslemGrupId");
                });

            migrationBuilder.CreateTable(
                name: "SM_HubTvConnection",
                columns: table => new
                {
                    HubTvConnectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TvId = table.Column<int>(type: "int", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConnectionStatus = table.Column<int>(type: "int", nullable: false),
                    IslemZamani = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_HubTvConnection", x => x.HubTvConnectionId);
                    table.ForeignKey(
                        name: "FK_SM_HubTvConnection_SM_Tvler_TvId",
                        column: x => x.TvId,
                        principalTable: "SM_Tvler",
                        principalColumn: "TvId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_TvBankolari",
                columns: table => new
                {
                    TvBankoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TvId = table.Column<int>(type: "int", nullable: false),
                    BankoId = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_TvBankolari", x => x.TvBankoId);
                    table.ForeignKey(
                        name: "FK_SM_TvBankolari_SM_Bankolar_BankoId",
                        column: x => x.BankoId,
                        principalTable: "SM_Bankolar",
                        principalColumn: "BankoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_TvBankolari_SM_Tvler_TvId",
                        column: x => x.TvId,
                        principalTable: "SM_Tvler",
                        principalColumn: "TvId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SM_BankolarKullanici",
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
                    table.PrimaryKey("PK_SM_BankolarKullanici", x => x.BankoKullaniciId);
                    table.ForeignKey(
                        name: "FK_SM_BankolarKullanici_SM_Bankolar_BankoId",
                        column: x => x.BankoId,
                        principalTable: "SM_Bankolar",
                        principalColumn: "BankoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_BankolarKullanici_SM_Personeller_PersonellerTcKimlikNo",
                        column: x => x.PersonellerTcKimlikNo,
                        principalTable: "SM_Personeller",
                        principalColumn: "TcKimlikNo");
                    table.ForeignKey(
                        name: "FK_SM_BankolarKullanici_SM_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "SM_Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SM_DatabaseLog",
                columns: table => new
                {
                    DatabaseLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatabaseAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeforeData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IslemZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonellerTcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_DatabaseLog", x => x.DatabaseLogId);
                    table.ForeignKey(
                        name: "FK_SM_DatabaseLog_SM_Personeller_PersonellerTcKimlikNo",
                        column: x => x.PersonellerTcKimlikNo,
                        principalTable: "SM_Personeller",
                        principalColumn: "TcKimlikNo");
                });

            migrationBuilder.CreateTable(
                name: "SM_HubConnection",
                columns: table => new
                {
                    HubConnectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConnectionStatus = table.Column<int>(type: "int", nullable: false),
                    IslemZamani = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_HubConnection", x => x.HubConnectionId);
                    table.ForeignKey(
                        name: "FK_SM_HubConnection_SM_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "SM_Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_PersonelYetkileri",
                columns: table => new
                {
                    PersonelYetkiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    YetkiId = table.Column<int>(type: "int", nullable: false),
                    YetkiTipleri = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModulControllerIslemlerControllerIslemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_PersonelYetkileri", x => x.PersonelYetkiId);
                    table.ForeignKey(
                        name: "FK_SM_PersonelYetkileri_SM_ModulControllerIslemler_ModulControllerIslemlerControllerIslemId",
                        column: x => x.ModulControllerIslemlerControllerIslemId,
                        principalTable: "SM_ModulControllerIslemler",
                        principalColumn: "ControllerIslemId");
                    table.ForeignKey(
                        name: "FK_SM_PersonelYetkileri_SM_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "SM_Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_PersonelYetkileri_SM_Yetkiler_YetkiId",
                        column: x => x.YetkiId,
                        principalTable: "SM_Yetkiler",
                        principalColumn: "YetkiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SM_KanalAltIslemleri",
                columns: table => new
                {
                    KanalAltIslemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KanalAltId = table.Column<int>(type: "int", nullable: false),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    KanalIslemId = table.Column<int>(type: "int", nullable: true),
                    KioskIslemGrupId = table.Column<int>(type: "int", nullable: true),
                    KanalAltIslemAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_KanalAltIslemleri", x => x.KanalAltIslemId);
                    table.ForeignKey(
                        name: "FK_SM_KanalAltIslemleri_SM_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "SM_HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_KanalAltIslemleri_SM_KanalIslemleri_KanalIslemId",
                        column: x => x.KanalIslemId,
                        principalTable: "SM_KanalIslemleri",
                        principalColumn: "KanalIslemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_KanalAltIslemleri_SM_KanallarAlt_KanalAltId",
                        column: x => x.KanalAltId,
                        principalTable: "SM_KanallarAlt",
                        principalColumn: "KanalAltId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SM_KanalAltIslemleri_SM_KioskIslemGruplari_KioskIslemGrupId",
                        column: x => x.KioskIslemGrupId,
                        principalTable: "SM_KioskIslemGruplari",
                        principalColumn: "KioskIslemGrupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SM_KanalPersonelleri",
                columns: table => new
                {
                    KanalPersonelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KanalAltIslemId = table.Column<int>(type: "int", nullable: false),
                    Uzmanlik = table.Column<int>(type: "int", nullable: false),
                    KanalAltIslemPersonelAktiflik = table.Column<int>(type: "int", nullable: false),
                    EklenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuzenlenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonellerTcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_KanalPersonelleri", x => x.KanalPersonelId);
                    table.ForeignKey(
                        name: "FK_SM_KanalPersonelleri_SM_KanalAltIslemleri_KanalAltIslemId",
                        column: x => x.KanalAltIslemId,
                        principalTable: "SM_KanalAltIslemleri",
                        principalColumn: "KanalAltIslemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_KanalPersonelleri_SM_Personeller_PersonellerTcKimlikNo",
                        column: x => x.PersonellerTcKimlikNo,
                        principalTable: "SM_Personeller",
                        principalColumn: "TcKimlikNo");
                    table.ForeignKey(
                        name: "FK_SM_KanalPersonelleri_SM_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "SM_Personeller",
                        principalColumn: "TcKimlikNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SM_Siralar",
                columns: table => new
                {
                    SiraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiraNo = table.Column<int>(type: "int", nullable: false),
                    KanalAltIslemId = table.Column<int>(type: "int", nullable: false),
                    KanalAltAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HizmetBinasiId = table.Column<int>(type: "int", nullable: false),
                    TcKimlikNo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SiraAlisZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IslemBaslamaZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IslemBitisZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BeklemeDurum = table.Column<int>(type: "int", nullable: false),
                    SiraAlisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SM_Siralar", x => x.SiraId);
                    table.ForeignKey(
                        name: "FK_SM_Siralar_SM_HizmetBinalari_HizmetBinasiId",
                        column: x => x.HizmetBinasiId,
                        principalTable: "SM_HizmetBinalari",
                        principalColumn: "HizmetBinasiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_Siralar_SM_KanalAltIslemleri_KanalAltIslemId",
                        column: x => x.KanalAltIslemId,
                        principalTable: "SM_KanalAltIslemleri",
                        principalColumn: "KanalAltIslemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SM_Siralar_SM_Personeller_TcKimlikNo",
                        column: x => x.TcKimlikNo,
                        principalTable: "SM_Personeller",
                        principalColumn: "TcKimlikNo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SM_AtanmaNedenleri_AtanmaNedeni",
                table: "SM_AtanmaNedenleri",
                column: "AtanmaNedeni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_BankoIslemleri_BankoIslemAdı",
                table: "SM_BankoIslemleri",
                column: "BankoIslemAdı",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_Bankolar_HizmetBinasiId_BankoNo_BankoAktiflik",
                table: "SM_Bankolar",
                columns: new[] { "HizmetBinasiId", "BankoNo", "BankoAktiflik" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_BankolarKullanici_BankoId",
                table: "SM_BankolarKullanici",
                column: "BankoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_BankolarKullanici_PersonellerTcKimlikNo",
                table: "SM_BankolarKullanici",
                column: "PersonellerTcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_SM_BankolarKullanici_TcKimlikNo",
                table: "SM_BankolarKullanici",
                column: "TcKimlikNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_DatabaseLog_PersonellerTcKimlikNo",
                table: "SM_DatabaseLog",
                column: "PersonellerTcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Departmanlar_DepartmanAdi",
                table: "SM_Departmanlar",
                column: "DepartmanAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_HizmetBinalari_DepartmanId",
                table: "SM_HizmetBinalari",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_HizmetBinalari_DepartmanlarDepartmanId",
                table: "SM_HizmetBinalari",
                column: "DepartmanlarDepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_HubConnection_TcKimlikNo",
                table: "SM_HubConnection",
                column: "TcKimlikNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_HubConnection_TcKimlikNo_ConnectionId_ConnectionStatus",
                table: "SM_HubConnection",
                columns: new[] { "TcKimlikNo", "ConnectionId", "ConnectionStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_SM_HubTvConnection_TvId",
                table: "SM_HubTvConnection",
                column: "TvId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_HubTvConnection_TvId_ConnectionId_ConnectionStatus",
                table: "SM_HubTvConnection",
                columns: new[] { "TvId", "ConnectionId", "ConnectionStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_SM_Ilceler_IlId",
                table: "SM_Ilceler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Iller_IlAdi",
                table: "SM_Iller",
                column: "IlAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalAltIslemleri_HizmetBinasiId_KanalAltId",
                table: "SM_KanalAltIslemleri",
                columns: new[] { "HizmetBinasiId", "KanalAltId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalAltIslemleri_KanalAltId",
                table: "SM_KanalAltIslemleri",
                column: "KanalAltId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalAltIslemleri_KanalIslemId",
                table: "SM_KanalAltIslemleri",
                column: "KanalIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalAltIslemleri_KioskIslemGrupId",
                table: "SM_KanalAltIslemleri",
                column: "KioskIslemGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalIslemleri_HizmetBinasiId_KanalId",
                table: "SM_KanalIslemleri",
                columns: new[] { "HizmetBinasiId", "KanalId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalIslemleri_KanalId",
                table: "SM_KanalIslemleri",
                column: "KanalId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Kanallar_KanalAdi",
                table: "SM_Kanallar",
                column: "KanalAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanallarAlt_KanalAltAdi",
                table: "SM_KanallarAlt",
                column: "KanalAltAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanallarAlt_KanallarKanalId",
                table: "SM_KanallarAlt",
                column: "KanallarKanalId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanallarAlt_KioskIslemGruplariKioskIslemGrupId",
                table: "SM_KanallarAlt",
                column: "KioskIslemGruplariKioskIslemGrupId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalPersonelleri_KanalAltIslemId_TcKimlikNo",
                table: "SM_KanalPersonelleri",
                columns: new[] { "KanalAltIslemId", "TcKimlikNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalPersonelleri_PersonellerTcKimlikNo",
                table: "SM_KanalPersonelleri",
                column: "PersonellerTcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_SM_KanalPersonelleri_TcKimlikNo",
                table: "SM_KanalPersonelleri",
                column: "TcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_SM_KioskGruplari_KioskGrupAdi",
                table: "SM_KioskGruplari",
                column: "KioskGrupAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_KioskIslemGruplari_HizmetBinasiId_KioskIslemGrupSira",
                table: "SM_KioskIslemGruplari",
                columns: new[] { "HizmetBinasiId", "KioskIslemGrupSira" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_KioskIslemGruplari_KioskGrupId_HizmetBinasiId",
                table: "SM_KioskIslemGruplari",
                columns: new[] { "KioskGrupId", "HizmetBinasiId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_ModulController_ModulId",
                table: "SM_ModulController",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_ModulControllerIslemler_ControllerId",
                table: "SM_ModulControllerIslemler",
                column: "ControllerId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_ModullerAlt_ModulId",
                table: "SM_ModullerAlt",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_PdksCihazlar_DepartmanId",
                table: "SM_PdksCihazlar",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_AtanmaNedeniId",
                table: "SM_Personeller",
                column: "AtanmaNedeniId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_DepartmanId",
                table: "SM_Personeller",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_EsininIsIlceId",
                table: "SM_Personeller",
                column: "EsininIsIlceId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_EsininIsIlId",
                table: "SM_Personeller",
                column: "EsininIsIlId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_HizmetBinasiId",
                table: "SM_Personeller",
                column: "HizmetBinasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_IlceId",
                table: "SM_Personeller",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_IlId",
                table: "SM_Personeller",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_SendikaId",
                table: "SM_Personeller",
                column: "SendikaId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_ServisId",
                table: "SM_Personeller",
                column: "ServisId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_SicilNo",
                table: "SM_Personeller",
                column: "SicilNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_TcKimlikNo",
                table: "SM_Personeller",
                column: "TcKimlikNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_Personeller_UnvanId",
                table: "SM_Personeller",
                column: "UnvanId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_PersonelYetkileri_ModulControllerIslemlerControllerIslemId",
                table: "SM_PersonelYetkileri",
                column: "ModulControllerIslemlerControllerIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_PersonelYetkileri_TcKimlikNo",
                table: "SM_PersonelYetkileri",
                column: "TcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_SM_PersonelYetkileri_YetkiId",
                table: "SM_PersonelYetkileri",
                column: "YetkiId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Sendikalar_SendikaAdi",
                table: "SM_Sendikalar",
                column: "SendikaAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_Servisler_ServisAdi",
                table: "SM_Servisler",
                column: "ServisAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_Siralar_HizmetBinasiId",
                table: "SM_Siralar",
                column: "HizmetBinasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Siralar_KanalAltIslemId",
                table: "SM_Siralar",
                column: "KanalAltIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Siralar_SiraNo_HizmetBinasiId_SiraAlisTarihi",
                table: "SM_Siralar",
                columns: new[] { "SiraNo", "HizmetBinasiId", "SiraAlisTarihi" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_Siralar_TcKimlikNo",
                table: "SM_Siralar",
                column: "TcKimlikNo");

            migrationBuilder.CreateIndex(
                name: "IX_SM_TvBankolari_BankoId",
                table: "SM_TvBankolari",
                column: "BankoId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_TvBankolari_TvId_BankoId",
                table: "SM_TvBankolari",
                columns: new[] { "TvId", "BankoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_Tvler_HizmetBinasiId",
                table: "SM_Tvler",
                column: "HizmetBinasiId");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Tvler_TvId_HizmetBinasiId",
                table: "SM_Tvler",
                columns: new[] { "TvId", "HizmetBinasiId" });

            migrationBuilder.CreateIndex(
                name: "IX_SM_Unvanlar_UnvanAdi",
                table: "SM_Unvanlar",
                column: "UnvanAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SM_Yetkiler_ActionAdi",
                table: "SM_Yetkiler",
                column: "ActionAdi");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Yetkiler_ControllerAdi",
                table: "SM_Yetkiler",
                column: "ControllerAdi");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Yetkiler_UstYetkiId_YetkiAdi",
                table: "SM_Yetkiler",
                columns: new[] { "UstYetkiId", "YetkiAdi" },
                unique: true,
                filter: "[UstYetkiId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SM_Yetkiler_YetkiAdi",
                table: "SM_Yetkiler",
                column: "YetkiAdi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SM_BankoIslemleri");

            migrationBuilder.DropTable(
                name: "SM_BankolarKullanici");

            migrationBuilder.DropTable(
                name: "SM_DatabaseLog");

            migrationBuilder.DropTable(
                name: "SM_HubConnection");

            migrationBuilder.DropTable(
                name: "SM_HubTvConnection");

            migrationBuilder.DropTable(
                name: "SM_KanalPersonelleri");

            migrationBuilder.DropTable(
                name: "SM_LoginLogoutLog");

            migrationBuilder.DropTable(
                name: "SM_ModullerAlt");

            migrationBuilder.DropTable(
                name: "SM_PdksCihazlar");

            migrationBuilder.DropTable(
                name: "SM_PersonelCocuklari");

            migrationBuilder.DropTable(
                name: "SM_PersonelYetkileri");

            migrationBuilder.DropTable(
                name: "SM_Siralar");

            migrationBuilder.DropTable(
                name: "SM_TvBankolari");

            migrationBuilder.DropTable(
                name: "SM_ModulControllerIslemler");

            migrationBuilder.DropTable(
                name: "SM_Yetkiler");

            migrationBuilder.DropTable(
                name: "SM_KanalAltIslemleri");

            migrationBuilder.DropTable(
                name: "SM_Personeller");

            migrationBuilder.DropTable(
                name: "SM_Bankolar");

            migrationBuilder.DropTable(
                name: "SM_Tvler");

            migrationBuilder.DropTable(
                name: "SM_ModulController");

            migrationBuilder.DropTable(
                name: "SM_KanalIslemleri");

            migrationBuilder.DropTable(
                name: "SM_KanallarAlt");

            migrationBuilder.DropTable(
                name: "SM_AtanmaNedenleri");

            migrationBuilder.DropTable(
                name: "SM_Ilceler");

            migrationBuilder.DropTable(
                name: "SM_Sendikalar");

            migrationBuilder.DropTable(
                name: "SM_Servisler");

            migrationBuilder.DropTable(
                name: "SM_Unvanlar");

            migrationBuilder.DropTable(
                name: "SM_Moduller");

            migrationBuilder.DropTable(
                name: "SM_Kanallar");

            migrationBuilder.DropTable(
                name: "SM_KioskIslemGruplari");

            migrationBuilder.DropTable(
                name: "SM_Iller");

            migrationBuilder.DropTable(
                name: "SM_HizmetBinalari");

            migrationBuilder.DropTable(
                name: "SM_KioskGruplari");

            migrationBuilder.DropTable(
                name: "SM_Departmanlar");
        }
    }
}
