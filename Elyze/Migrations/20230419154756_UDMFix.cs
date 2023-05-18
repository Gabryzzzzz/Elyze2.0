using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class UDMFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUDM",
                table: "CampiMa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CampiMa_IdUDM",
                table: "CampiMa",
                column: "IdUDM");

            migrationBuilder.AddForeignKey(
                name: "FK_CampiMa_UnitaMisura_IdUDM",
                table: "CampiMa",
                column: "IdUDM",
                principalTable: "UnitaMisura",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CampiMa_UnitaMisura_IdUDM",
                table: "CampiMa");

            migrationBuilder.DropIndex(
                name: "IX_CampiMa_IdUDM",
                table: "CampiMa");

            migrationBuilder.DropColumn(
                name: "IdUDM",
                table: "CampiMa");
        }
    }
}
