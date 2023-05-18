// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations.User
{
    public partial class SeedingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "24DD64C7-EB35-4DBE-935A-B285FDCA0B27", "c940dc7c-f4ff-4d98-832d-fb32e22dea89", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3DDBE80D-AC50-4288-8CEB-4298FF4C37F2", "43f1d098-ac72-4bba-8390-1d8b4802ecec", "Standard", "STANDARD" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24DD64C7-EB35-4DBE-935A-B285FDCA0B27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3DDBE80D-AC50-4288-8CEB-4298FF4C37F2");

            migrationBuilder.DropColumn(
                name: "Attivo",
                table: "AspNetUsers");
        }
    }
}
