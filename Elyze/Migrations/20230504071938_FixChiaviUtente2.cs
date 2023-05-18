using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class FixChiaviUtente2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdValidatore",
                table: "InserimentiFissi",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdUtente",
                table: "InserimentiFissi",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_InserimentiFissi_IdUtente",
                table: "InserimentiFissi",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_InserimentiFissi_IdValidatore",
                table: "InserimentiFissi",
                column: "IdValidatore");

            migrationBuilder.AddForeignKey(
                name: "FK_InserimentiFissi_AspNetUsers_IdUtente",
                table: "InserimentiFissi",
                column: "IdUtente",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InserimentiFissi_AspNetUsers_IdValidatore",
                table: "InserimentiFissi",
                column: "IdValidatore",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InserimentiFissi_AspNetUsers_IdUtente",
                table: "InserimentiFissi");

            migrationBuilder.DropForeignKey(
                name: "FK_InserimentiFissi_AspNetUsers_IdValidatore",
                table: "InserimentiFissi");

            migrationBuilder.DropIndex(
                name: "IX_InserimentiFissi_IdUtente",
                table: "InserimentiFissi");

            migrationBuilder.DropIndex(
                name: "IX_InserimentiFissi_IdValidatore",
                table: "InserimentiFissi");

            migrationBuilder.AlterColumn<string>(
                name: "IdValidatore",
                table: "InserimentiFissi",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdUtente",
                table: "InserimentiFissi",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
