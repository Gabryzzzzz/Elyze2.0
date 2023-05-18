using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class FixSeed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 1,
            //    column: "LocalPath",
            //    value: "Confindustria/acqua.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 2,
            //    column: "LocalPath",
            //    value: "Confindustria/emissioni.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 3,
            //    column: "LocalPath",
            //    value: "Confindustria/energia.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 4,
            //    column: "LocalPath",
            //    value: "Confindustria/finanza.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 5,
            //    column: "LocalPath",
            //    value: "Confindustria/innovazione.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 6,
            //    column: "LocalPath",
            //    value: "Confindustria/logistica.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 7,
            //    column: "LocalPath",
            //    value: "Confindustria/persone.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 8,
            //    column: "LocalPath",
            //    value: "Confindustria/rifiuti.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 9,
            //    column: "LocalPath",
            //    value: "Confindustria/salute.svg");

            //migrationBuilder.UpdateData(
            //    table: "AreaIcons",
            //    keyColumn: "Id",
            //    keyValue: 10,
            //    column: "LocalPath",
            //    value: "Confindustria/sito-produttivo.svg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocalPath",
                value: "Confindustria/acqua..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocalPath",
                value: "Confindustria/emissioni..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocalPath",
                value: "Confindustria/energia..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 4,
                column: "LocalPath",
                value: "Confindustria/finanza..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 5,
                column: "LocalPath",
                value: "Confindustria/innovazione..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 6,
                column: "LocalPath",
                value: "Confindustria/logistica..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 7,
                column: "LocalPath",
                value: "Confindustria/persone..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 8,
                column: "LocalPath",
                value: "Confindustria/rifiuti..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 9,
                column: "LocalPath",
                value: "Confindustria/salute..svg");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 10,
                column: "LocalPath",
                value: "Confindustria/sito-produttivo..svg");
        }
    }
}
