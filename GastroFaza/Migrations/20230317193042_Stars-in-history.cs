using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroFaza.Migrations
{
    public partial class Starsinhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stars",
                table: "Histories",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stars",
                table: "Histories");
        }
    }
}
