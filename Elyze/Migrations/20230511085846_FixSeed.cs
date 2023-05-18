// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class FixSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "AreaIcons",
            //    columns: new[] { "Id", "Codice", "CreatedAt", "CreatedBy", "Descrizione", "LocalPath", "UpdatedAt", "UpdatedBy" },
            //    values: new object[,]
            //    {
            //        { 1, "01", null, null, "Acqua", "Confindustria/acqua.png", null, null },
            //        { 2, "02", null, null, "Emissioni", "Confindustria/emissioni.png", null, null },
            //        { 3, "03", null, null, "Energia", "Confindustria/energia.png", null, null },
            //        { 4, "04", null, null, "Finanza", "Confindustria/finanza.png", null, null },
            //        { 5, "05", null, null, "Innovazione", "Confindustria/innovazione.png", null, null },
            //        { 6, "06", null, null, "Logistica", "Confindustria/logistica.png", null, null },
            //        { 7, "07", null, null, "Persone", "Confindustria/persone.png", null, null },
            //        { 8, "08", null, null, "Report", "Confindustria/acqua.report", null, null },
            //        { 9, "09", null, null, "Rifiuti", "Confindustria/rifiuti.png", null, null },
            //        { 10, "10", null, null, "Salute", "Confindustria/salute.png", null, null },
            //        { 11, "11", null, null, "Sito produttivo", "Confindustria/sito-produttivo.png", null, null }
            //    });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
