using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class FixMAeCampiMA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdMicroArea",
                table: "MicroArea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCampiMa",
                table: "CampiMa",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdMicroArea",
                table: "MicroArea");

            migrationBuilder.DropColumn(
                name: "IdCampiMa",
                table: "CampiMa");
        }
    }
}
