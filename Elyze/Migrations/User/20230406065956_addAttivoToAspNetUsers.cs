using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations.User
{
    public partial class addAttivoToAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attivo",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attivo",
                table: "AspNetUsers");
        }
    }
}
