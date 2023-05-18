// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class UnitaDiMisura
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["UDM"].Value,
                    BackRoute = $"{basePath}/Home/Impostazioni",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["UDM"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["UDM"].Value}", Link = $"{basePath}/UnitaMisura/Create" }
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["UDM_Add"].Value}",
                    BackRoute = $"{basePath}/UnitaMisura",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["UDM"].Value}", $"{basePath}/UnitaMisura", $"{sharedLocalizer["Create"].Value}" })
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Details"].Value}  {sharedLocalizer["UDM"].Value}",
                    BackRoute = $"{basePath}/UnitaMisura",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["UDM"].Value}", $"{basePath}/UnitaMisura", $"{sharedLocalizer["Details"].Value}" })
                };
                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Edit"].Value} {sharedLocalizer["UDM"].Value}",
                    BackRoute = $"{basePath}/UnitaMisura",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["UDM"].Value}", $"{basePath}/UnitaMisura", $"{sharedLocalizer["Edit"].Value}" })
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Delete"].Value} {sharedLocalizer["UDM"].Value}",
                    BackRoute = $"{basePath}/UnitaMisura",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["UDM"].Value}", $"{basePath}/UnitaMisura", $"{sharedLocalizer["Delete"].Value}" })
                };

                return header;
            }

        }
    }
}
