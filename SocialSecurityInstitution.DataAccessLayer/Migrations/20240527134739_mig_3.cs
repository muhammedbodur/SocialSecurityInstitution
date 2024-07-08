using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialSecurityInstitution.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KanalAltIslemleri_HizmetBinasiId",
                table: "KanalAltIslemleri");

            migrationBuilder.CreateIndex(
                name: "IX_KanalAltIslemleri_HizmetBinasiId_KanalAltId",
                table: "KanalAltIslemleri",
                columns: new[] { "HizmetBinasiId", "KanalAltId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KanalAltIslemleri_HizmetBinasiId_KanalAltId",
                table: "KanalAltIslemleri");

            migrationBuilder.CreateIndex(
                name: "IX_KanalAltIslemleri_HizmetBinasiId",
                table: "KanalAltIslemleri",
                column: "HizmetBinasiId");
        }
    }
}
