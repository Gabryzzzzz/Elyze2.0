using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class RemovedSedeFromSocieta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sede",
                table: "Societa");

            migrationBuilder.AlterColumn<int>(
                name: "GruppoId",
                table: "Societa",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocalPath",
                value: "Normale/acqua.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocalPath",
                value: "Normale/emissioni.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocalPath",
                value: "Normale/energia.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 4,
                column: "LocalPath",
                value: "Normale/finanza.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 5,
                column: "LocalPath",
                value: "Normale/innovazione.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 6,
                column: "LocalPath",
                value: "Normale/logistica.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 7,
                column: "LocalPath",
                value: "Normale/persone.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 8,
                column: "LocalPath",
                value: "Normale/rifiuti.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 9,
                column: "LocalPath",
                value: "Normale/salute.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 10,
                column: "LocalPath",
                value: "Normale/sito-produttivo.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GruppoId",
                table: "Societa",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<string>(
                name: "Sede",
                table: "Societa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocalPath",
                value: "Confindustria/acqua.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocalPath",
                value: "Confindustria/emissioni.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocalPath",
                value: "Confindustria/energia.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 4,
                column: "LocalPath",
                value: "Confindustria/finanza.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 5,
                column: "LocalPath",
                value: "Confindustria/innovazione.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 6,
                column: "LocalPath",
                value: "Confindustria/logistica.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 7,
                column: "LocalPath",
                value: "Confindustria/persone.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 8,
                column: "LocalPath",
                value: "Confindustria/rifiuti.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 9,
                column: "LocalPath",
                value: "Confindustria/salute.svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 10,
                column: "LocalPath",
                value: "Confindustria/sito-produttivo.svg");
        }
    }
}
