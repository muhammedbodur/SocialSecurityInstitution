using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialSecurityInstitution.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TcKimlikNo",
                table: "Siralar",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Siralar_TcKimlikNo",
                table: "Siralar",
                column: "TcKimlikNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Siralar_Personeller_TcKimlikNo",
                table: "Siralar",
                column: "TcKimlikNo",
                principalTable: "Personeller",
                principalColumn: "TcKimlikNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siralar_Personeller_TcKimlikNo",
                table: "Siralar");

            migrationBuilder.DropIndex(
                name: "IX_Siralar_TcKimlikNo",
                table: "Siralar");

            migrationBuilder.DropColumn(
                name: "TcKimlikNo",
                table: "Siralar");
        }
    }
}
