using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class TipologiaNormalizationCampiMA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TipologieCampiMicroArea",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Descrizione", "Nome", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, null, null, "", "Decimal", null, null });

            migrationBuilder.InsertData(
                table: "TipologieCampiMicroArea",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Descrizione", "Nome", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 2, null, null, "", "Integer", null, null });

            migrationBuilder.InsertData(
                table: "TipologieCampiMicroArea",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Descrizione", "Nome", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 3, null, null, "", "Text", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TipologieCampiMicroArea",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipologieCampiMicroArea",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipologieCampiMicroArea",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
