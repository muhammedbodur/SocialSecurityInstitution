using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialSecurityInstitution.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HubConnection_Personeller_TcKimlikNo",
                table: "HubConnection");

            migrationBuilder.DropIndex(
                name: "IX_HubConnection_TcKimlikNo",
                table: "HubConnection");

            migrationBuilder.AlterColumn<string>(
                name: "TcKimlikNo",
                table: "HubConnection",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HubConnection_TcKimlikNo",
                table: "HubConnection",
                column: "TcKimlikNo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HubConnection_Personeller_TcKimlikNo",
                table: "HubConnection",
                column: "TcKimlikNo",
                principalTable: "Personeller",
                principalColumn: "TcKimlikNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HubConnection_Personeller_TcKimlikNo",
                table: "HubConnection");

            migrationBuilder.DropIndex(
                name: "IX_HubConnection_TcKimlikNo",
                table: "HubConnection");

            migrationBuilder.AlterColumn<string>(
                name: "TcKimlikNo",
                table: "HubConnection",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_HubConnection_TcKimlikNo",
                table: "HubConnection",
                column: "TcKimlikNo",
                unique: true,
                filter: "[TcKimlikNo] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_HubConnection_Personeller_TcKimlikNo",
                table: "HubConnection",
                column: "TcKimlikNo",
                principalTable: "Personeller",
                principalColumn: "TcKimlikNo");
        }
    }
}
