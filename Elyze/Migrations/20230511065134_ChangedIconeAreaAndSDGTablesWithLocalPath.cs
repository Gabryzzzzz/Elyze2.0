using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class ChangedIconeAreaAndSDGTablesWithLocalPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Base64",
                table: "SDG",
                newName: "LocalPath");

            migrationBuilder.RenameColumn(
                name: "Base64",
                table: "AreaIcons",
                newName: "LocalPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocalPath",
                table: "SDG",
                newName: "Base64");

            migrationBuilder.RenameColumn(
                name: "LocalPath",
                table: "AreaIcons",
                newName: "Base64");
        }
    }
}
