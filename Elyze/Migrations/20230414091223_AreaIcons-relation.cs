using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class AreaIconsrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconaArea",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "IconaSdg",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "Lingua",
                table: "Area");

            migrationBuilder.AlterColumn<bool>(
                name: "Stato",
                table: "Area",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(0)))",
                comment: "Attiva o non attiva",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(0)))");

            migrationBuilder.AlterColumn<int>(
                name: "IdArea",
                table: "Area",
                type: "int",
                nullable: false,
                comment: "Con 'IdLingua' forma la chiave composta della tabella",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdIconaArea",
                table: "Area",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Le aree hanno una sola icona, quindi la relazione è uno a uno");

            migrationBuilder.AddColumn<int>(
                name: "IdLingua",
                table: "Area",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AreaIcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Base64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaIcons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Area_IdIconaArea",
                table: "Area",
                column: "IdIconaArea");

            migrationBuilder.CreateIndex(
                name: "IX_Area_IdLingua",
                table: "Area",
                column: "IdLingua");

            migrationBuilder.AddForeignKey(
                name: "FK_Area_AreaIcons_IdIconaArea",
                table: "Area",
                column: "IdIconaArea",
                principalTable: "AreaIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Area_Lingue_IdLingua",
                table: "Area",
                column: "IdLingua",
                principalTable: "Lingue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Area_AreaIcons_IdIconaArea",
                table: "Area");

            migrationBuilder.DropForeignKey(
                name: "FK_Area_Lingue_IdLingua",
                table: "Area");

            migrationBuilder.DropTable(
                name: "AreaIcons");

            migrationBuilder.DropIndex(
                name: "IX_Area_IdIconaArea",
                table: "Area");

            migrationBuilder.DropIndex(
                name: "IX_Area_IdLingua",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "IdIconaArea",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "IdLingua",
                table: "Area");

            migrationBuilder.AlterColumn<bool>(
                name: "Stato",
                table: "Area",
                type: "bit",
                nullable: false,
                defaultValueSql: "(CONVERT([bit],(0)))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "(CONVERT([bit],(0)))",
                oldComment: "Attiva o non attiva");

            migrationBuilder.AlterColumn<int>(
                name: "IdArea",
                table: "Area",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Con 'IdLingua' forma la chiave composta della tabella");

            migrationBuilder.AddColumn<string>(
                name: "IconaArea",
                table: "Area",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconaSdg",
                table: "Area",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lingua",
                table: "Area",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
