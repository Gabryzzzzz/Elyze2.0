// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class SeededIconeAreaESDG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AreaIcons",
                columns: new[] { "Id", "Codice", "CreatedAt", "CreatedBy", "Descrizione", "LocalPath", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "01", null, null, "Acqua", "Confindustria/acqua.svg", null, null },
                    { 2, "02", null, null, "Emissioni", "Confindustria/emissioni.svg", null, null },
                    { 3, "03", null, null, "Energia", "Confindustria/energia.svg", null, null },
                    { 4, "04", null, null, "Finanza", "Confindustria/finanza.svg", null, null },
                    { 5, "05", null, null, "Innovazione", "Confindustria/innovazione.svg", null, null },
                    { 6, "06", null, null, "Logistica", "Confindustria/logistica.svg", null, null },
                    { 7, "07", null, null, "Persone", "Confindustria/persone.svg", null, null },
                    { 8, "08", null, null, "Report", "Confindustria/acqua.svg", null, null },
                    { 9, "09", null, null, "Rifiuti", "Confindustria/rifiuti.svg", null, null },
                    { 10, "10", null, null, "Salute", "Confindustria/salute.svg", null, null },
                    { 11, "11", null, null, "Sito produttivo", "Confindustria/sito-produttivo.svg", null, null }
                });

            migrationBuilder.InsertData(
                table: "SDG",
                columns: new[] { "Id", "Codice", "CreatedAt", "CreatedBy", "Descrizione", "LocalPath", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "01", null, null, "No poverty", "01-NoPoverty.png", null, null },
                    { 2, "02", null, null, "Zero hunger", "02-ZeroHunger.png", null, null },
                    { 3, "03", null, null, "Good health and well being", "03-GoodHealthAndWellBeing.png", null, null },
                    { 4, "04", null, null, "Quality education", "04-QualityEducation.png", null, null },
                    { 5, "05", null, null, "Gender equality", "05-GenderEquality.png", null, null },
                    { 6, "06", null, null, "Clean water and sanitation", "06-CleanWaterAndSanitation.png", null, null },
                    { 7, "07", null, null, "Affordable and clean energy", "07-AffordableAndCleanEnergy.png", null, null },
                    { 8, "08", null, null, "Decent work and economy growth", "08-DecentWorkAndEconomyGrowth.png", null, null },
                    { 9, "09", null, null, "Industry innovation and infrastructure", "09-IndustryInnovationAndInfrastructure.png", null, null },
                    { 10, "10", null, null, "Reduce inequalities", "10-ReduceInequalities.png", null, null },
                    { 11, "11", null, null, "Sustainable cities and communities", "11-SustainableCitiesAndCommunities.png", null, null },
                    { 12, "12", null, null, "Responsible consumption and production", "12-ResponsibleConsumptionAndProduction.png", null, null },
                    { 13, "13", null, null, "Climate action", "13-ClimateAction.png", null, null },
                    { 14, "14", null, null, "Life below water", "14-LifeBelowWater.png", null, null },
                    { 15, "15", null, null, "Life and land", "15-LifeAndLand.png", null, null },
                    { 16, "16", null, null, "Peace, justice and strong institution", "16-PeaceJusticheAndStrongInsitution.png", null, null },
                    { 17, "17", null, null, "Partnership for the goals", "17-PartnershipForTheGoals.png", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AreaIcons",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SDG",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
