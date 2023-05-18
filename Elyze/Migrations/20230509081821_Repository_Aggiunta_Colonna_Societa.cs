using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class Repository_Aggiunta_Colonna_Societa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SocietaId",
                table: "Repository",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Repository_SocietaId",
                table: "Repository",
                column: "SocietaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_Societa_SocietaId",
                table: "Repository",
                column: "SocietaId",
                principalTable: "Societa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repository_Societa_SocietaId",
                table: "Repository");

            migrationBuilder.DropIndex(
                name: "IX_Repository_SocietaId",
                table: "Repository");

            migrationBuilder.DropColumn(
                name: "SocietaId",
                table: "Repository");
        }
    }
}
