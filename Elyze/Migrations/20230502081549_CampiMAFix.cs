using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class CampiMAFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StabilimentoId",
                table: "InserimentiFissi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StabilimentoId",
                table: "InserimentiFissi",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
