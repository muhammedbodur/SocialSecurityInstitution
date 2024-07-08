using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialSecurityInstitution.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siralar",
                columns: table => new
                {
                    SiraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sira = table.Column<int>(type: "int", nullable: false),
                    BankoIslemId = table.Column<int>(type: "int", nullable: true),
                    KanalAltIslemId = table.Column<int>(type: "int", nullable: false),
                    SiraAlisZamani = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IslemBaslamaZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IslemBitisZamani = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BeklemeDurum = table.Column<int>(type: "int", nullable: false),
                    SiraAlisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siralar", x => x.SiraId);
                    table.ForeignKey(
                        name: "FK_Siralar_BankoIslemleri_BankoIslemId",
                        column: x => x.BankoIslemId,
                        principalTable: "BankoIslemleri",
                        principalColumn: "BankoIslemId");
                    table.ForeignKey(
                        name: "FK_Siralar_KanalAltIslemleri_KanalAltIslemId",
                        column: x => x.KanalAltIslemId,
                        principalTable: "KanalAltIslemleri",
                        principalColumn: "KanalAltIslemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KioskIslemGruplari_KioskIslemGrupSira",
                table: "KioskIslemGruplari",
                column: "KioskIslemGrupSira",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Siralar_BankoIslemId",
                table: "Siralar",
                column: "BankoIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_Siralar_KanalAltIslemId",
                table: "Siralar",
                column: "KanalAltIslemId");

            migrationBuilder.CreateIndex(
                name: "IX_Siralar_Sira_KanalAltIslemId_SiraAlisTarihi",
                table: "Siralar",
                columns: new[] { "Sira", "KanalAltIslemId", "SiraAlisTarihi" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Siralar");

            migrationBuilder.DropIndex(
                name: "IX_KioskIslemGruplari_KioskIslemGrupSira",
                table: "KioskIslemGruplari");
        }
    }
}
