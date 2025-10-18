using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddingFKForClubxJoiningClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "ClubJoiningRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClubJoiningRequests_ClubId",
                table: "ClubJoiningRequests",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubJoiningRequests_Clubs_ClubId",
                table: "ClubJoiningRequests",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubJoiningRequests_Clubs_ClubId",
                table: "ClubJoiningRequests");

            migrationBuilder.DropIndex(
                name: "IX_ClubJoiningRequests_ClubId",
                table: "ClubJoiningRequests");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "ClubJoiningRequests");
        }
    }
}
