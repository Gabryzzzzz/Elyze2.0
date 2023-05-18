using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class Add_TemplateMail_TipologiaMail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipologiaMail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipologiaMail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMailId = table.Column<int>(type: "int", nullable: false),
                    TemplateOggetto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateCorpo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMail_TipologiaMail_TipoMailId",
                        column: x => x.TipoMailId,
                        principalTable: "TipologiaMail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipologiaMail",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Descrizione", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, null, null, "ResetPassword", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMail_TipoMailId",
                table: "TemplateMail",
                column: "TipoMailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateMail");

            migrationBuilder.DropTable(
                name: "TipologiaMail");
        }
    }
}
