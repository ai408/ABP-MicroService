using Microsoft.EntityFrameworkCore.Migrations;

namespace Business.Migrations
{
    public partial class Namespace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Namespace",
                table: "base_form",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Namespace",
                table: "base_form");
        }
    }
}
