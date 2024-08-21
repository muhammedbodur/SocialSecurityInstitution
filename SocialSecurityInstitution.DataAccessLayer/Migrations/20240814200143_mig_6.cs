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
            migrationBuilder.AddForeignKey(
                name: "FK_HubConnection_Personeller_TcKimlikNo",
                table: "HubConnection",
                column: "TcKimlikNo",
                principalTable: "Personeller",
                principalColumn: "TcKimlikNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HubConnection_Personeller_TcKimlikNo",
                table: "HubConnection");
        }
    }
}
