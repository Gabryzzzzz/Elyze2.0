// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class Inserimenti
        {

            public static HeaderClass ImportaInserimenti(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Import"].Value,
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Download_CSV"].Value}", Link = $"{basePath}/Gen/DownloadCSV" },
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Import"].Value}" }),
                    BackRoute = $"{basePath}/Home/Index"
                };

                return header;
            }

            public static HeaderClass DownloadCSV(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Download_CSV"].Value,
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Import"].Value}", $"{basePath}/Gen/ImportaInserimenti", $"{sharedLocalizer["Download_CSV"].Value}" }),
                    BackRoute = $"{basePath}/Gen/ImportaInserimenti"
                };

                return header;
            }

            public static HeaderClass ExportMultiplo(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Export_Multiplo"].Value,
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Export_Multiplo"].Value}" }),
                    BackRoute = $"{basePath}/Home/Index"
                };

                return header;
            }

            public static HeaderClass IndexPopolamentoMA(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath, int id, int idArea)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_Data"].Value,
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea/main/{idArea}", $"{sharedLocalizer["MA_Populate"].Value}" }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Inserimenti_New"].Value}", Link = $"{basePath}/Gen/AggiungiInserimento/{id}" },
                    BackRoute = $"{basePath}/MicroArea/main/{idArea}"
                };

                return header;
            }

            public static HeaderClass CreateInserimento(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath, int id, int idArea)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_Data"].Value,
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea/main/{idArea}", $"{sharedLocalizer["MA_Populate"].Value}", $"{basePath}/Gen/IndexInserimenti/{id}", sharedLocalizer["MA_Populate"].Value }),
                    BackRoute = $"{basePath}/Gen/IndexInserimenti/{id}"
                };

                return header;
            }

            public static HeaderClass EditInserimento(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath, int id, int idArea)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["MA_Data"].Value} {sharedLocalizer["Edit"].Value}",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea/main/{idArea}", $"{sharedLocalizer["MA_Populate"].Value}", $"{basePath}/Gen/IndexInserimenti/{id}", sharedLocalizer["Edit"].Value }),
                    BackRoute = $"{basePath}/Gen/IndexInserimenti/{id}"
                };

                return header;
            }

            public static HeaderClass DetailsInserimento(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath, int id, int idArea)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["MA_Data"].Value} {sharedLocalizer["Details"].Value}",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea/main/{idArea}", $"{sharedLocalizer["MA_Populate"].Value}", $"{basePath}/Gen/IndexInserimenti/{id}", sharedLocalizer["Details"].Value }),
                    BackRoute = $"{basePath}/Gen/IndexInserimenti/{id}"
                };

                return header;
            }

            public static HeaderClass DeleteInserimento(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath, int id, int idArea)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["MA_Data"].Value} {sharedLocalizer["Delete"].Value}",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea/main/{idArea}", $"{sharedLocalizer["MA_Populate"].Value}", $"{basePath}/Gen/IndexInserimenti/{id}", sharedLocalizer["Delete"].Value }),
                    BackRoute = $"{basePath}/Gen/IndexInserimenti/{id}"
                };

                return header;
            }
        }
    }

}
