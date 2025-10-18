using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddingClubRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Token_TokenId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Token",
                table: "Token");

            migrationBuilder.RenameTable(
                name: "Token",
                newName: "Tokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClubsRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubsRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubsRequests_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubsRequests_ClubId",
                table: "ClubsRequests",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tokens_TokenId",
                table: "Users",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tokens_TokenId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ClubsRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tokens",
                table: "Tokens");

            migrationBuilder.RenameTable(
                name: "Tokens",
                newName: "Token");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Token",
                table: "Token",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Token_TokenId",
                table: "Users",
                column: "TokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
