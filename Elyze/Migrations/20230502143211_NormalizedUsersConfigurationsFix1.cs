using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class NormalizedUsersConfigurationsFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSocieta");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSede");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserReparto");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserGruppo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserArea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserSocieta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserSede",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserReparto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserGruppo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserArea",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
