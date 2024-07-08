using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialSecurityInstitution.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KioskIslemGruplari_KioskIslemGrupAdi_KioskIslemGrupAktiflik",
                table: "KioskIslemGruplari");

            migrationBuilder.DropColumn(
                name: "KioskIslemGrupAdi",
                table: "KioskIslemGruplari");

            migrationBuilder.AddColumn<int>(
                name: "HizmetBinasiId",
                table: "KioskIslemGruplari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KioskGrupId",
                table: "KioskIslemGruplari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KioskGruplari",
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
                    table.PrimaryKey("PK_KioskGruplari", x => x.KioskGrupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KioskIslemGruplari_HizmetBinasiId",
                table: "KioskIslemGruplari",
                column: "HizmetBinasiId");

            migrationBuilder.CreateIndex(
                name: "IX_KioskIslemGruplari_KioskGrupId_KioskIslemGrupAktiflik",
                table: "KioskIslemGruplari",
                columns: new[] { "KioskGrupId", "KioskIslemGrupAktiflik" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KioskGruplari_KioskGrupAdi",
                table: "KioskGruplari",
                column: "KioskGrupAdi",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KioskIslemGruplari_HizmetBinalari_HizmetBinasiId",
                table: "KioskIslemGruplari",
                column: "HizmetBinasiId",
                principalTable: "HizmetBinalari",
                principalColumn: "HizmetBinasiId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KioskIslemGruplari_KioskGruplari_KioskGrupId",
                table: "KioskIslemGruplari",
                column: "KioskGrupId",
                principalTable: "KioskGruplari",
                principalColumn: "KioskGrupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KioskIslemGruplari_HizmetBinalari_HizmetBinasiId",
                table: "KioskIslemGruplari");

            migrationBuilder.DropForeignKey(
                name: "FK_KioskIslemGruplari_KioskGruplari_KioskGrupId",
                table: "KioskIslemGruplari");

            migrationBuilder.DropTable(
                name: "KioskGruplari");

            migrationBuilder.DropIndex(
                name: "IX_KioskIslemGruplari_HizmetBinasiId",
                table: "KioskIslemGruplari");

            migrationBuilder.DropIndex(
                name: "IX_KioskIslemGruplari_KioskGrupId_KioskIslemGrupAktiflik",
                table: "KioskIslemGruplari");

            migrationBuilder.DropColumn(
                name: "HizmetBinasiId",
                table: "KioskIslemGruplari");

            migrationBuilder.DropColumn(
                name: "KioskGrupId",
                table: "KioskIslemGruplari");

            migrationBuilder.AddColumn<string>(
                name: "KioskIslemGrupAdi",
                table: "KioskIslemGruplari",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_KioskIslemGruplari_KioskIslemGrupAdi_KioskIslemGrupAktiflik",
                table: "KioskIslemGruplari",
                columns: new[] { "KioskIslemGrupAdi", "KioskIslemGrupAktiflik" },
                unique: true);
        }
    }
}
