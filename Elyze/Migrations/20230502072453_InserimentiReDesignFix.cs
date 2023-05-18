using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class InserimentiReDesignFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "InserimentiFissi",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InserimentiFissi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "InserimentiFissi",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "InserimentiFissi",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "InserimentiFissi");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InserimentiFissi");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "InserimentiFissi");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "InserimentiFissi");
        }
    }
}
