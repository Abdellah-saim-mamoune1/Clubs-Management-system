using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddingOpenForRegistrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OpenForRegistrations",
                table: "Clubs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenForRegistrations",
                table: "Clubs");
        }
    }
}
