using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareMind2.Migrations
{
    public partial class Desk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "availability",
                table: "Desks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "availability",
                table: "Desks");
        }
    }
}
