using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class SistematiMicroAreaECampiMA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLingua",
                table: "MicroArea");

            migrationBuilder.DropColumn(
                name: "MicroAreaId",
                table: "MicroArea");

            migrationBuilder.DropColumn(
                name: "IdCampo",
                table: "CampiMa");

            migrationBuilder.DropColumn(
                name: "Lingua",
                table: "CampiMa");

            migrationBuilder.DropColumn(
                name: "UDM",
                table: "CampiMa");

            migrationBuilder.DropColumn(
                name: "UDMId",
                table: "CampiMa");

            migrationBuilder.RenameColumn(
                name: "MicroAreaId",
                table: "CampiMa",
                newName: "IdMicroArea");

            migrationBuilder.CreateIndex(
                name: "IX_CampiMa_IdMicroArea",
                table: "CampiMa",
                column: "IdMicroArea");

            migrationBuilder.AddForeignKey(
                name: "FK_CampiMa_MicroArea_IdMicroArea",
                table: "CampiMa",
                column: "IdMicroArea",
                principalTable: "MicroArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampiMa_MicroArea_IdMicroArea",
                table: "CampiMa");

            migrationBuilder.DropIndex(
                name: "IX_CampiMa_IdMicroArea",
                table: "CampiMa");

            migrationBuilder.RenameColumn(
                name: "IdMicroArea",
                table: "CampiMa",
                newName: "MicroAreaId");

            migrationBuilder.AddColumn<int>(
                name: "IdLingua",
                table: "MicroArea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MicroAreaId",
                table: "MicroArea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCampo",
                table: "CampiMa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Lingua",
                table: "CampiMa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "(N'')");

            migrationBuilder.AddColumn<string>(
                name: "UDM",
                table: "CampiMa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UDMId",
                table: "CampiMa",
                type: "int",
                nullable: true);
        }
    }
}
