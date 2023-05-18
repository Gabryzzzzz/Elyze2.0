using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class InserimentiReDesignFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InserimentiFissi_Inserimenti_IdInserimento",
                table: "InserimentiFissi");

            migrationBuilder.DropIndex(
                name: "IX_InserimentiFissi_IdInserimento",
                table: "InserimentiFissi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InserimentiFissi_IdInserimento",
                table: "InserimentiFissi",
                column: "IdInserimento");

            migrationBuilder.AddForeignKey(
                name: "FK_InserimentiFissi_Inserimenti_IdInserimento",
                table: "InserimentiFissi",
                column: "IdInserimento",
                principalTable: "Inserimenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
