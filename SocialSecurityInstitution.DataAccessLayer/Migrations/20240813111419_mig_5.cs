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
            migrationBuilder.AddColumn<int>(
                name: "HizmetBinasiId",
                table: "Siralar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Siralar_HizmetBinasiId",
                table: "Siralar",
                column: "HizmetBinasiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Siralar_HizmetBinalari_HizmetBinasiId",
                table: "Siralar",
                column: "HizmetBinasiId",
                principalTable: "HizmetBinalari",
                principalColumn: "HizmetBinasiId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siralar_HizmetBinalari_HizmetBinasiId",
                table: "Siralar");

            migrationBuilder.DropIndex(
                name: "IX_Siralar_HizmetBinasiId",
                table: "Siralar");

            migrationBuilder.DropColumn(
                name: "HizmetBinasiId",
                table: "Siralar");
        }
    }
}
