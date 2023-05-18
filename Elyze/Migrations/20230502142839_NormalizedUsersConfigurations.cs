// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class NormalizedUsersConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGruppo_Gruppo_GruppoId",
                table: "UserGruppo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGruppo_User_UserId",
                table: "UserGruppo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReparto_Reparto_RepartoId",
                table: "UserReparto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReparto_User_UserId",
                table: "UserReparto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSede_Sede_SedeId",
                table: "UserSede");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSede_User_UserId",
                table: "UserSede");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocieta_Societa_SocietaId",
                table: "UserSocieta");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocieta_User_UserId",
                table: "UserSocieta");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserConfig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSocieta",
                table: "UserSocieta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSede",
                table: "UserSede");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReparto",
                table: "UserReparto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGruppo",
                table: "UserGruppo");

            migrationBuilder.RenameColumn(
                name: "SocietaId",
                table: "UserSocieta",
                newName: "IdSocieta");

            migrationBuilder.RenameIndex(
                name: "IX_UserSocieta_SocietaId",
                table: "UserSocieta",
                newName: "IX_UserSocieta_IdSocieta");

            migrationBuilder.RenameColumn(
                name: "SedeId",
                table: "UserSede",
                newName: "IdSede");

            migrationBuilder.RenameIndex(
                name: "IX_UserSede_SedeId",
                table: "UserSede",
                newName: "IX_UserSede_IdSede");

            migrationBuilder.RenameColumn(
                name: "RepartoId",
                table: "UserReparto",
                newName: "IdReparto");

            migrationBuilder.RenameIndex(
                name: "IX_UserReparto_RepartoId",
                table: "UserReparto",
                newName: "IX_UserReparto_IdReparto");

            migrationBuilder.RenameColumn(
                name: "GruppoId",
                table: "UserGruppo",
                newName: "IdGruppo");

            migrationBuilder.RenameIndex(
                name: "IX_UserGruppo_GruppoId",
                table: "UserGruppo",
                newName: "IX_UserGruppo_IdGruppo");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserSocieta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserSocieta",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserSocieta",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "UserSocieta",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "UserSocieta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserSede",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserSede",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserSede",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "UserSede",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "UserSede",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserReparto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserReparto",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserReparto",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "UserReparto",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserGruppo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserGruppo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserGruppo",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "UserGruppo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "UserArea",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSocieta",
                table: "UserSocieta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSede",
                table: "UserSede",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReparto",
                table: "UserReparto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGruppo",
                table: "UserGruppo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocieta_IdUser",
                table: "UserSocieta",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserSede_IdUser",
                table: "UserSede",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserReparto_IdUser",
                table: "UserReparto",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserGruppo_IdUser",
                table: "UserGruppo",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserArea_IdArea",
                table: "UserArea",
                column: "IdArea");

            migrationBuilder.CreateIndex(
                name: "IX_UserArea_IdUser",
                table: "UserArea",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserArea_Area_IdArea",
                table: "UserArea",
                column: "IdArea",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserArea_AspNetUsers_IdUser",
                table: "UserArea",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGruppo_AspNetUsers_IdUser",
                table: "UserGruppo",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGruppo_Gruppo_IdGruppo",
                table: "UserGruppo",
                column: "IdGruppo",
                principalTable: "Gruppo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReparto_AspNetUsers_IdUser",
                table: "UserReparto",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReparto_Reparto_IdReparto",
                table: "UserReparto",
                column: "IdReparto",
                principalTable: "Reparto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSede_AspNetUsers_IdUser",
                table: "UserSede",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSede_Sede_IdSede",
                table: "UserSede",
                column: "IdSede",
                principalTable: "Sede",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocieta_AspNetUsers_IdUser",
                table: "UserSocieta",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocieta_Societa_IdSocieta",
                table: "UserSocieta",
                column: "IdSocieta",
                principalTable: "Societa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserArea_Area_IdArea",
                table: "UserArea");

            migrationBuilder.DropForeignKey(
                name: "FK_UserArea_AspNetUsers_IdUser",
                table: "UserArea");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGruppo_AspNetUsers_IdUser",
                table: "UserGruppo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGruppo_Gruppo_IdGruppo",
                table: "UserGruppo");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReparto_AspNetUsers_IdUser",
                table: "UserReparto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReparto_Reparto_IdReparto",
                table: "UserReparto");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSede_AspNetUsers_IdUser",
                table: "UserSede");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSede_Sede_IdSede",
                table: "UserSede");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocieta_AspNetUsers_IdUser",
                table: "UserSocieta");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocieta_Societa_IdSocieta",
                table: "UserSocieta");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSocieta",
                table: "UserSocieta");

            migrationBuilder.DropIndex(
                name: "IX_UserSocieta_IdUser",
                table: "UserSocieta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSede",
                table: "UserSede");

            migrationBuilder.DropIndex(
                name: "IX_UserSede_IdUser",
                table: "UserSede");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReparto",
                table: "UserReparto");

            migrationBuilder.DropIndex(
                name: "IX_UserReparto_IdUser",
                table: "UserReparto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGruppo",
                table: "UserGruppo");

            migrationBuilder.DropIndex(
                name: "IX_UserGruppo_IdUser",
                table: "UserGruppo");

            migrationBuilder.DropIndex(
                name: "IX_UserArea_IdArea",
                table: "UserArea");

            migrationBuilder.DropIndex(
                name: "IX_UserArea_IdUser",
                table: "UserArea");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserSocieta");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "UserSocieta");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "UserSocieta");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserSede");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "UserSede");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "UserSede");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserReparto");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "UserReparto");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserGruppo");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "UserGruppo");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "UserArea");

            migrationBuilder.RenameColumn(
                name: "IdSocieta",
                table: "UserSocieta",
                newName: "SocietaId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSocieta_IdSocieta",
                table: "UserSocieta",
                newName: "IX_UserSocieta_SocietaId");

            migrationBuilder.RenameColumn(
                name: "IdSede",
                table: "UserSede",
                newName: "SedeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSede_IdSede",
                table: "UserSede",
                newName: "IX_UserSede_SedeId");

            migrationBuilder.RenameColumn(
                name: "IdReparto",
                table: "UserReparto",
                newName: "RepartoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReparto_IdReparto",
                table: "UserReparto",
                newName: "IX_UserReparto_RepartoId");

            migrationBuilder.RenameColumn(
                name: "IdGruppo",
                table: "UserGruppo",
                newName: "GruppoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGruppo_IdGruppo",
                table: "UserGruppo",
                newName: "IX_UserGruppo_GruppoId");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserSocieta",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserSocieta",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserSede",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserSede",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserReparto",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserReparto",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "UserGruppo",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserGruppo",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSocieta",
                table: "UserSocieta",
                columns: new[] { "UserId", "SocietaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSede",
                table: "UserSede",
                columns: new[] { "UserId", "SedeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReparto",
                table: "UserReparto",
                columns: new[] { "UserId", "RepartoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGruppo",
                table: "UserGruppo",
                columns: new[] { "UserId", "GruppoId" });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    Attivo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))"),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Id_Area = table.Column<int>(type: "int", nullable: false),
                    Id_Gruppo = table.Column<int>(type: "int", nullable: false),
                    Id_Plant = table.Column<int>(type: "int", nullable: false),
                    Id_Reparto = table.Column<int>(type: "int", nullable: false),
                    Id_Societa = table.Column<int>(type: "int", nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfig", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserGruppo_Gruppo_GruppoId",
                table: "UserGruppo",
                column: "GruppoId",
                principalTable: "Gruppo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGruppo_User_UserId",
                table: "UserGruppo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReparto_Reparto_RepartoId",
                table: "UserReparto",
                column: "RepartoId",
                principalTable: "Reparto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReparto_User_UserId",
                table: "UserReparto",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSede_Sede_SedeId",
                table: "UserSede",
                column: "SedeId",
                principalTable: "Sede",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSede_User_UserId",
                table: "UserSede",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocieta_Societa_SocietaId",
                table: "UserSocieta",
                column: "SocietaId",
                principalTable: "Societa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocieta_User_UserId",
                table: "UserSocieta",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
