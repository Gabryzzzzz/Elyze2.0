using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class FixesAndNormalizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipologia",
                table: "CampiMa");

            migrationBuilder.AlterColumn<string>(
                name: "Nome_Tabella",
                table: "MicroArea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<int>(
                name: "IdTipologia",
                table: "CampiMa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipologieCampiMicroArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipologieCampiMicroArea", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampiMa_IdTipologia",
                table: "CampiMa",
                column: "IdTipologia");

            migrationBuilder.AddForeignKey(
                name: "FK_CampiMa_TipologieCampiMicroArea_IdTipologia",
                table: "CampiMa",
                column: "IdTipologia",
                principalTable: "TipologieCampiMicroArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampiMa_TipologieCampiMicroArea_IdTipologia",
                table: "CampiMa");

            migrationBuilder.DropTable(
                name: "TipologieCampiMicroArea");

            migrationBuilder.DropIndex(
                name: "IX_CampiMa_IdTipologia",
                table: "CampiMa");

            migrationBuilder.DropColumn(
                name: "IdTipologia",
                table: "CampiMa");

            migrationBuilder.AlterColumn<string>(
                name: "Nome_Tabella",
                table: "MicroArea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<string>(
                name: "Tipologia",
                table: "CampiMa",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
