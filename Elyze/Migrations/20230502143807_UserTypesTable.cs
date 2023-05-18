using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class UserTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUserType",
                table: "UserArea",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserTypesArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypesArea", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserTypesArea",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, null, null, "", "Validator", null, null });

            migrationBuilder.InsertData(
                table: "UserTypesArea",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 2, null, null, "", "Compiler", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_UserArea_IdUserType",
                table: "UserArea",
                column: "IdUserType");

            migrationBuilder.AddForeignKey(
                name: "FK_UserArea_UserTypesArea_IdUserType",
                table: "UserArea",
                column: "IdUserType",
                principalTable: "UserTypesArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserArea_UserTypesArea_IdUserType",
                table: "UserArea");

            migrationBuilder.DropTable(
                name: "UserTypesArea");

            migrationBuilder.DropIndex(
                name: "IX_UserArea_IdUserType",
                table: "UserArea");

            migrationBuilder.DropColumn(
                name: "IdUserType",
                table: "UserArea");
        }
    }
}
