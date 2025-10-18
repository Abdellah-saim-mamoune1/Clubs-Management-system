using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class updatingClubRequesClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedClubs_Users_UserId",
                table: "RequestedClubs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RequestedClubs",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedClubs_UserId",
                table: "RequestedClubs",
                newName: "IX_RequestedClubs_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedClubs_Users_StudentId",
                table: "RequestedClubs",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedClubs_Users_StudentId",
                table: "RequestedClubs");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "RequestedClubs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestedClubs_StudentId",
                table: "RequestedClubs",
                newName: "IX_RequestedClubs_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedClubs_Users_UserId",
                table: "RequestedClubs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
