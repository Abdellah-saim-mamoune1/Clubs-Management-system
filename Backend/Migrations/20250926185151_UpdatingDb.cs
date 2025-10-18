using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_UserClubs_UserClubId",
                table: "Clubs");

            migrationBuilder.DropTable(
                name: "UserUserClub");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_UserClubId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "IsViewed",
                table: "UserEvents");

            migrationBuilder.DropColumn(
                name: "UserClubId",
                table: "Clubs");

            migrationBuilder.AddColumn<DateOnly>(
                name: "JoinedAt",
                table: "UserClubs",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Clubs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_UserClubs_ClubId",
                table: "UserClubs",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClubs_UserId",
                table: "UserClubs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClubs_Clubs_ClubId",
                table: "UserClubs",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClubs_Users_UserId",
                table: "UserClubs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClubs_Clubs_ClubId",
                table: "UserClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClubs_Users_UserId",
                table: "UserClubs");

            migrationBuilder.DropIndex(
                name: "IX_UserClubs_ClubId",
                table: "UserClubs");

            migrationBuilder.DropIndex(
                name: "IX_UserClubs_UserId",
                table: "UserClubs");

            migrationBuilder.DropColumn(
                name: "JoinedAt",
                table: "UserClubs");

            migrationBuilder.AddColumn<bool>(
                name: "IsViewed",
                table: "UserEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Clubs",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "UserClubId",
                table: "Clubs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserUserClub",
                columns: table => new
                {
                    UserClubsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserClub", x => new { x.UserClubsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserUserClub_UserClubs_UserClubsId",
                        column: x => x.UserClubsId,
                        principalTable: "UserClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUserClub_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_UserClubId",
                table: "Clubs",
                column: "UserClubId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUserClub_UsersId",
                table: "UserUserClub",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_UserClubs_UserClubId",
                table: "Clubs",
                column: "UserClubId",
                principalTable: "UserClubs",
                principalColumn: "Id");
        }
    }
}
