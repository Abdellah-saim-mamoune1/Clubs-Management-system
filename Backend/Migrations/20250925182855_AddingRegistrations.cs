using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddingRegistrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ClubsRequests");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Events",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptRegistration",
                table: "ClubsRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ClubsRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "ClubsRequests",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "ClubsRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxRegistrationsCount",
                table: "ClubsRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "ClubsRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventsRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxRegistrationsCount = table.Column<int>(type: "int", nullable: false),
                    CurrentRegistrationsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsRegistrations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_RegistrationId",
                table: "Events",
                column: "RegistrationId",
                unique: true,
                filter: "[RegistrationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventsRegistrations_RegistrationId",
                table: "Events",
                column: "RegistrationId",
                principalTable: "EventsRegistrations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventsRegistrations_RegistrationId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventsRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_Events_RegistrationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AcceptRegistration",
                table: "ClubsRequests");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ClubsRequests");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ClubsRequests");

            migrationBuilder.DropColumn(
                name: "From",
                table: "ClubsRequests");

            migrationBuilder.DropColumn(
                name: "MaxRegistrationsCount",
                table: "ClubsRequests");

            migrationBuilder.DropColumn(
                name: "To",
                table: "ClubsRequests");

            migrationBuilder.AlterColumn<int>(
                name: "ClubId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "CreatedAt",
                table: "ClubsRequests",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
