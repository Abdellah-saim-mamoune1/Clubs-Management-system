using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddingRequestedClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestedClubId",
                table: "Clubs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestedClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClubTypeId = table.Column<int>(type: "int", nullable: false),
                    ClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedClubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestedClubs_ClubTypes_ClubTypeId",
                        column: x => x.ClubTypeId,
                        principalTable: "ClubTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestedClubs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_RequestedClubId",
                table: "Clubs",
                column: "RequestedClubId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedClubs_ClubTypeId",
                table: "RequestedClubs",
                column: "ClubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestedClubs_UserId",
                table: "RequestedClubs",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_RequestedClubs_RequestedClubId",
                table: "Clubs",
                column: "RequestedClubId",
                principalTable: "RequestedClubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_RequestedClubs_RequestedClubId",
                table: "Clubs");

            migrationBuilder.DropTable(
                name: "RequestedClubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_RequestedClubId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "RequestedClubId",
                table: "Clubs");
        }
    }
}
