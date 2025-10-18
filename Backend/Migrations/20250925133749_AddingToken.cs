using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddingToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TokenId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TokenId",
                table: "Users",
                column: "TokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Token_TokenId",
                table: "Users",
                column: "TokenId",
                principalTable: "Token",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Token_TokenId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Users_TokenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Users");
        }
    }
}
