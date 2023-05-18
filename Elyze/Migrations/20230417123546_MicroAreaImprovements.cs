using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class MicroAreaImprovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lingua",
                table: "MicroArea");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "MicroArea",
                newName: "IdLingua");

            migrationBuilder.AlterColumn<int>(
                name: "Id_Gri",
                table: "MicroArea",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdArea",
                table: "MicroArea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MicroArea_Id_Gri",
                table: "MicroArea",
                column: "Id_Gri");

            migrationBuilder.CreateIndex(
                name: "IX_MicroArea_IdArea",
                table: "MicroArea",
                column: "IdArea");

            migrationBuilder.AddForeignKey(
                name: "FK_MicroArea_Area_IdArea",
                table: "MicroArea",
                column: "IdArea",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MicroArea_Gri_Id_Gri",
                table: "MicroArea",
                column: "Id_Gri",
                principalTable: "Gri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MicroArea_Area_IdArea",
                table: "MicroArea");

            migrationBuilder.DropForeignKey(
                name: "FK_MicroArea_Gri_Id_Gri",
                table: "MicroArea");

            migrationBuilder.DropIndex(
                name: "IX_MicroArea_Id_Gri",
                table: "MicroArea");

            migrationBuilder.DropIndex(
                name: "IX_MicroArea_IdArea",
                table: "MicroArea");

            migrationBuilder.DropColumn(
                name: "IdArea",
                table: "MicroArea");

            migrationBuilder.RenameColumn(
                name: "IdLingua",
                table: "MicroArea",
                newName: "AreaId");

            migrationBuilder.AlterColumn<string>(
                name: "Id_Gri",
                table: "MicroArea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Lingua",
                table: "MicroArea",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
