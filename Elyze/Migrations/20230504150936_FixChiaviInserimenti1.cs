using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class FixChiaviInserimenti1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inserimenti_IdCampo",
                table: "Inserimenti",
                column: "IdCampo");

            migrationBuilder.AddForeignKey(
                name: "FK_Inserimenti_CampiMa_IdCampo",
                table: "Inserimenti",
                column: "IdCampo",
                principalTable: "CampiMa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inserimenti_CampiMa_IdCampo",
                table: "Inserimenti");

            migrationBuilder.DropIndex(
                name: "IX_Inserimenti_IdCampo",
                table: "Inserimenti");
        }
    }
}
