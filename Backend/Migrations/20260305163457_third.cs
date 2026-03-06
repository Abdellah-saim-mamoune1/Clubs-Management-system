using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubsRequests_Clubs_ClubId",
                table: "ClubsRequests");

            migrationBuilder.DropTable(
                name: "EventImages");

            migrationBuilder.DropIndex(
                name: "IX_ClubsRequests_ClubId",
                table: "ClubsRequests");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "RequestedClubs");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Clubs");

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "RequestedClubs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "RequestedClubs",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Events",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Clubs",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "RequestedClubs");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "RequestedClubs");

            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Clubs");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "RequestedClubs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMainImage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventImages_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubsRequests_ClubId",
                table: "ClubsRequests",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_EventImages_EventId",
                table: "EventImages",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubsRequests_Clubs_ClubId",
                table: "ClubsRequests",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
