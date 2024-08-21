using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialSecurityInstitution.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Siralar_SiraNo_SiraAlisTarihi",
                table: "Siralar");

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionId",
                table: "HubConnection",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Siralar_SiraNo_HizmetBinasiId_SiraAlisTarihi",
                table: "Siralar",
                columns: new[] { "SiraNo", "HizmetBinasiId", "SiraAlisTarihi" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HubConnection_TcKimlikNo_ConnectionId_ConnectionStatus",
                table: "HubConnection",
                columns: new[] { "TcKimlikNo", "ConnectionId", "ConnectionStatus" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Siralar_SiraNo_HizmetBinasiId_SiraAlisTarihi",
                table: "Siralar");

            migrationBuilder.DropIndex(
                name: "IX_HubConnection_TcKimlikNo_ConnectionId_ConnectionStatus",
                table: "HubConnection");

            migrationBuilder.AlterColumn<string>(
                name: "ConnectionId",
                table: "HubConnection",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Siralar_SiraNo_SiraAlisTarihi",
                table: "Siralar",
                columns: new[] { "SiraNo", "SiraAlisTarihi" },
                unique: true);
        }
    }
}
