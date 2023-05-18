using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class FixSeedingIcone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Descrizione", "LocalPath" },
                values: new object[] { "Rifiuti", "Confindustria/rifiuti.png" });

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Descrizione", "LocalPath" },
                values: new object[] { "Salute", "Confindustria/salute.png" });

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Descrizione", "LocalPath" },
                values: new object[] { "Sito produttivo", "Confindustria/sito-produttivo.png" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Descrizione", "LocalPath" },
                values: new object[] { "Report", "Confindustria/acqua.report" });

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Descrizione", "LocalPath" },
                values: new object[] { "Rifiuti", "Confindustria/rifiuti.png" });

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Descrizione", "LocalPath" },
                values: new object[] { "Salute", "Confindustria/salute.png" });

            migrationBuilder.InsertData(
                table: "AreaIcons",
                columns: new[] { "Id", "Codice", "CreatedAt", "CreatedBy", "Descrizione", "LocalPath", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 11, "11", null, null, "Sito produttivo", "Confindustria/sito-produttivo.png", null, null });
        }
    }
}
