using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Rents",
                newName: "TenantId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Rents",
                newName: "UserId");
        }
    }
}
