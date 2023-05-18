using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class InserimentiReDesignFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MicroAreaId",
                table: "Inserimenti");

            migrationBuilder.AddColumn<int>(
                name: "IdMicroArea",
                table: "InserimentiFissi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_InserimentiFissi_IdMicroArea",
                table: "InserimentiFissi",
                column: "IdMicroArea");

            migrationBuilder.AddForeignKey(
                name: "FK_InserimentiFissi_MicroArea_IdMicroArea",
                table: "InserimentiFissi",
                column: "IdMicroArea",
                principalTable: "MicroArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InserimentiFissi_MicroArea_IdMicroArea",
                table: "InserimentiFissi");

            migrationBuilder.DropIndex(
                name: "IX_InserimentiFissi_IdMicroArea",
                table: "InserimentiFissi");

            migrationBuilder.DropColumn(
                name: "IdMicroArea",
                table: "InserimentiFissi");

            migrationBuilder.AddColumn<int>(
                name: "MicroAreaId",
                table: "Inserimenti",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
