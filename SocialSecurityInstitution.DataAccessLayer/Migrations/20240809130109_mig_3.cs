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
            migrationBuilder.DropColumn(
                name: "ConnectionStatus",
                table: "Personeller");

            migrationBuilder.AddColumn<int>(
                name: "ConnectionStatus",
                table: "HubConnection",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionStatus",
                table: "HubConnection");

            migrationBuilder.AddColumn<int>(
                name: "ConnectionStatus",
                table: "Personeller",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
