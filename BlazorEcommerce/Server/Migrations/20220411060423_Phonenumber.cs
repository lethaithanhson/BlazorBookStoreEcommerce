using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Migrations
{
    public partial class Phonenumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Addresses");
        }
    }
}
