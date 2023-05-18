using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class FixChiaviUtente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UtenteId",
                table: "InserimentiFissi");

            migrationBuilder.DropColumn(
                name: "ValidatoreId",
                table: "InserimentiFissi");

            migrationBuilder.AddColumn<string>(
                name: "IdUtente",
                table: "InserimentiFissi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdValidatore",
                table: "InserimentiFissi",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUtente",
                table: "InserimentiFissi");

            migrationBuilder.DropColumn(
                name: "IdValidatore",
                table: "InserimentiFissi");

            migrationBuilder.AddColumn<int>(
                name: "UtenteId",
                table: "InserimentiFissi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValidatoreId",
                table: "InserimentiFissi",
                type: "int",
                nullable: true);
        }
    }
}
