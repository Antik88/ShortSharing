using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortSharing.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeusersrents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Things_Users_UserId",
                table: "Things");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Things_UserId",
                table: "Things");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RenterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThingId = table.Column<Guid>(type: "uuid", nullable: false),
                    EndRentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartRentDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_Things_ThingId",
                        column: x => x.ThingId,
                        principalTable: "Things",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Users_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Things_OwnerId",
                table: "Things",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_RenterId",
                table: "Rents",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_ThingId",
                table: "Rents",
                column: "ThingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Things_Users_OwnerId",
                table: "Things",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
