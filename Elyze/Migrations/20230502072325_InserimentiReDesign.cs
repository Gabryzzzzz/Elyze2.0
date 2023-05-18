using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class InserimentiReDesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFine",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "DataInizio",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "DataInserimento",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "DataValidazione",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "RepartoId",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "SocietaId",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "StabilimentoId",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "Stato",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "UtenteId",
                table: "Inserimenti");

            migrationBuilder.DropColumn(
                name: "ValidatoreId",
                table: "Inserimenti");

            migrationBuilder.CreateTable(
                name: "InserimentiFissi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    ValidatoreId = table.Column<int>(type: "int", nullable: true),
                    DataInizio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataInserimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataValidazione = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stato = table.Column<int>(type: "int", nullable: true),
                    RepartoId = table.Column<int>(type: "int", nullable: false),
                    SocietaId = table.Column<int>(type: "int", nullable: false),
                    StabilimentoId = table.Column<int>(type: "int", nullable: false),
                    IdInserimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InserimentiFissi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InserimentiFissi_Inserimenti_IdInserimento",
                        column: x => x.IdInserimento,
                        principalTable: "Inserimenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InserimentiFissi_IdInserimento",
                table: "InserimentiFissi",
                column: "IdInserimento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InserimentiFissi");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFine",
                table: "Inserimenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInizio",
                table: "Inserimenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInserimento",
                table: "Inserimenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataValidazione",
                table: "Inserimenti",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RepartoId",
                table: "Inserimenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SocietaId",
                table: "Inserimenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StabilimentoId",
                table: "Inserimenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stato",
                table: "Inserimenti",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtenteId",
                table: "Inserimenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValidatoreId",
                table: "Inserimenti",
                type: "int",
                nullable: true);
        }
    }
}
