using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class AggiunteChiaviGerarchie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ValutaId",
                table: "Sede",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Societa_GruppoId",
                table: "Societa",
                column: "GruppoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sede_SocietaId",
                table: "Sede",
                column: "SocietaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sede_ValutaId",
                table: "Sede",
                column: "ValutaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sede_Societa_SocietaId",
                table: "Sede",
                column: "SocietaId",
                principalTable: "Societa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sede_Valuta_ValutaId",
                table: "Sede",
                column: "ValutaId",
                principalTable: "Valuta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Societa_Gruppo_GruppoId",
                table: "Societa",
                column: "GruppoId",
                principalTable: "Gruppo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sede_Societa_SocietaId",
                table: "Sede");

            migrationBuilder.DropForeignKey(
                name: "FK_Sede_Valuta_ValutaId",
                table: "Sede");

            migrationBuilder.DropForeignKey(
                name: "FK_Societa_Gruppo_GruppoId",
                table: "Societa");

            migrationBuilder.DropIndex(
                name: "IX_Societa_GruppoId",
                table: "Societa");

            migrationBuilder.DropIndex(
                name: "IX_Sede_SocietaId",
                table: "Sede");

            migrationBuilder.DropIndex(
                name: "IX_Sede_ValutaId",
                table: "Sede");

            migrationBuilder.AlterColumn<int>(
                name: "ValutaId",
                table: "Sede",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
