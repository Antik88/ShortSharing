using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Service.DLL.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserPicture",
                table: "Users",
                newName: "UserPictureUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserPictureUrl",
                table: "Users",
                newName: "UserPicture");
        }
    }
}
