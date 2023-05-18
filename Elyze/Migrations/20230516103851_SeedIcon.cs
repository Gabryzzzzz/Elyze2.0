using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class SeedIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sede_Valuta_ValutaId",
                table: "Sede");

            migrationBuilder.DropIndex(
                name: "IX_Sede_ValutaId",
                table: "Sede");

            migrationBuilder.DropColumn(
                name: "ValutaId",
                table: "Sede");

            migrationBuilder.AlterColumn<string>(
                name: "LocalPath",
                table: "AreaIcons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocalPath",
                value: "{0}/acqua.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocalPath",
                value: "{0}/emissioni.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocalPath",
                value: "{0}/energia.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 4,
                column: "LocalPath",
                value: "{0}/finanza.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 5,
                column: "LocalPath",
                value: "{0}/innovazione.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 6,
                column: "LocalPath",
                value: "{0}/logistica.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 7,
                column: "LocalPath",
                value: "{0}/persone.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 8,
                column: "LocalPath",
                value: "{0}/rifiuti.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 9,
                column: "LocalPath",
                value: "{0}/salute.{1}");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 10,
                column: "LocalPath",
                value: "{0}/sito-produttivo.{1}");

            migrationBuilder.InsertData(
                table: "Gri",
                columns: new[] { "Id", "CodiceGri", "CreatedAt", "CreatedBy", "DataCreazione", "DataSpegnimento", "Stato", "Titolo", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "201-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 201 Performance economiche 2016", null, null },
                    { 2, "201-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 201 Performance economiche 2016", null, null },
                    { 3, "201-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 201 Performance economiche 2016", null, null },
                    { 4, "201-4", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 201 Performance economiche 2016", null, null },
                    { 5, "202-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 202 Presenza sul mercato 2016", null, null },
                    { 6, "202-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 202 Presenza sul mercato 2016", null, null },
                    { 7, "203-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 203 Impatti economici indiretti 2016", null, null },
                    { 8, "203-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 203 Impatti economici indiretti 2016", null, null },
                    { 9, "204-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 204 Prassi di approvvigionamento 2016", null, null },
                    { 10, "205-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 205 Anticorruzione 2016", null, null },
                    { 11, "205-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 205 Anticorruzione 2016", null, null },
                    { 12, "205-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 205 Anticorruzione 2016", null, null },
                    { 13, "206-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 206 Comportamento anticoncorrenziale 2016", null, null },
                    { 14, "207-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 207 Tasse 2019", null, null },
                    { 15, "207-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 207 Tasse 2019", null, null },
                    { 16, "207-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 207 Tasse 2019", null, null },
                    { 17, "207-4", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 207 Tasse 2019", null, null },
                    { 18, "301-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 301 Materiali 2016", null, null },
                    { 19, "301-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 301 Materiali 2016", null, null },
                    { 20, "301-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 301 Materiali 2016", null, null },
                    { 21, "302-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 302 Energia 2016", null, null },
                    { 22, "302-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 302 Energia 2016", null, null },
                    { 23, "302-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 302 Energia 2016", null, null },
                    { 24, "302-4", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 302 Energia 2016", null, null },
                    { 25, "302-5", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 302 Energia 2016", null, null },
                    { 26, "303-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 303 Acqua ed effluenti 2018", null, null },
                    { 27, "303-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 303 Acqua ed effluenti 2018", null, null },
                    { 28, "303-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 303 Acqua ed effluenti 2018", null, null },
                    { 29, "303-4", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 303 Acqua ed effluenti 2018", null, null },
                    { 30, "303-5", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 303 Acqua ed effluenti 2018", null, null },
                    { 31, "304-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 304 Biodiversità 2016", null, null },
                    { 32, "304-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 304 Biodiversità 2016", null, null }
                });

            migrationBuilder.InsertData(
                table: "Gri",
                columns: new[] { "Id", "CodiceGri", "CreatedAt", "CreatedBy", "DataCreazione", "DataSpegnimento", "Stato", "Titolo", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 33, "304-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 304 Biodiversità 2016", null, null },
                    { 34, "304-4", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 304 Biodiversità 2016", null, null },
                    { 35, "305-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 305 Emissioni 2016", null, null },
                    { 36, "305-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 305 Emissioni 2016", null, null },
                    { 37, "305-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 305 Emissioni 2016", null, null },
                    { 38, "305-4", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 305 Emissioni 2016", null, null },
                    { 39, "305-5", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 305 Emissioni 2016", null, null },
                    { 40, "305-6", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 305 Emissioni 2016", null, null },
                    { 41, "305-7", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 305 Emissioni 2016", null, null },
                    { 42, "306-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 306 Rifiuti 2020", null, null },
                    { 43, "306-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 306 Rifiuti 2020", null, null },
                    { 44, "306-3 (2020)", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 306 Rifiuti 2020", null, null },
                    { 45, "306-4 (2020)", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 306 Rifiuti 2020", null, null },
                    { 46, "306-5 (2020)", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 306 Rifiuti 2020", null, null },
                    { 47, "306-3 (2016)", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 306 Scarichi idrici e rifiuti 2016 ", null, null },
                    { 48, "306-4 (2016)", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 306 Scarichi idrici e rifiuti 2016 ", null, null },
                    { 49, "306-5 (2016)", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 306 Scarichi idrici e rifiuti 2016", null, null },
                    { 50, "308-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 308 Valutazione ambientale dei fornitori 2016", null, null },
                    { 51, "308-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 308 Valutazione ambientale dei fornitori 2016", null, null },
                    { 52, "401-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 401 Occupazione 2016", null, null },
                    { 53, "401-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 401 Occupazione 2016", null, null },
                    { 54, "401-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 401 Occupazione 2016", null, null },
                    { 55, "402-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 402 Gestione del lavoro e delle relazioni sindacali 2016", null, null },
                    { 56, "403-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 57, "403-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 58, "403-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 59, "403-4", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 60, "403-5", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 61, "403-6", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 62, "403-7", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 63, "403-8", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 64, "403-9", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 65, "403-10", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 403 Salute e sicurezza sul lavoro 2018", null, null },
                    { 66, "404-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 404 Formazione e istruzione 2016", null, null },
                    { 67, "404-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 404 Formazione e istruzione 2016", null, null },
                    { 68, "404-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 404 Formazione e istruzione 2016", null, null },
                    { 69, "405-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 405 Diversità e pari opportunità 2016", null, null },
                    { 70, "405-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 405 Diversità e pari opportunità 2016", null, null },
                    { 71, "406-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 406 Non discriminazione 2016", null, null },
                    { 72, "407-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 407 Libertà di associazione e contrattazione collettiva 2016", null, null },
                    { 73, "408-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 408 Lavoro minorile 2016", null, null },
                    { 74, "409-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 409 Lavoro forzato o obbligatorio 2016", null, null }
                });

            migrationBuilder.InsertData(
                table: "Gri",
                columns: new[] { "Id", "CodiceGri", "CreatedAt", "CreatedBy", "DataCreazione", "DataSpegnimento", "Stato", "Titolo", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 75, "410-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 410 Pratiche per la sicurezza 2016", null, null },
                    { 76, "411-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 411 Diritti dei popoli indigeni 2016", null, null },
                    { 77, "413-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 413 Comunità locali 2016", null, null },
                    { 78, "413-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 413 Comunità locali 2016", null, null },
                    { 79, "414-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 414 Valutazione sociale dei fornitori", null, null },
                    { 80, "414-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 414 Valutazione sociale dei fornitori", null, null },
                    { 81, "415-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 415 Politica pubblica 2016", null, null },
                    { 82, "416-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 416 Salute e sicurezza dei clienti 2016", null, null },
                    { 83, "416-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 416 Salute e sicurezza dei clienti 2016", null, null },
                    { 84, "417-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 417 Marketing ed etichettatura 2016", null, null },
                    { 85, "417-2", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 417 Marketing ed etichettatura 2016", null, null },
                    { 86, "417-3", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 417 Marketing ed etichettatura 2016", null, null },
                    { 87, "418-1", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "GRI 418 Privacy dei clienti 2016", null, null }
                });

            migrationBuilder.InsertData(
                table: "Lingue",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Descrizione", "Sigla", "SiglaEstesa", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, "SYSTEM", "Italia", "IT", "it-IT", null, "SYSTEM" },
                    { 2, null, "SYSTEM", "Inglese", "EN", "en-EN", null, "SYSTEM" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Gri",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Lingue",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lingue",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "ValutaId",
                table: "Sede",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LocalPath",
                table: "AreaIcons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocalPath",
                value: "Normale/acqua.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocalPath",
                value: "Normale/emissioni.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocalPath",
                value: "Normale/energia.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 4,
                column: "LocalPath",
                value: "Normale/finanza.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 5,
                column: "LocalPath",
                value: "Normale/innovazione.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 6,
                column: "LocalPath",
                value: "Normale/logistica.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 7,
                column: "LocalPath",
                value: "Normale/persone.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 8,
                column: "LocalPath",
                value: "Normale/rifiuti.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 9,
                column: "LocalPath",
                value: "Normale/salute.png");

            migrationBuilder.UpdateData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 10,
                column: "LocalPath",
                value: "Normale/sito-produttivo.png");

            migrationBuilder.CreateIndex(
                name: "IX_Sede_ValutaId",
                table: "Sede",
                column: "ValutaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sede_Valuta_ValutaId",
                table: "Sede",
                column: "ValutaId",
                principalTable: "Valuta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
