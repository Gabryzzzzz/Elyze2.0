// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class Area
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Area"].Value,
                    BackRoute = $"{basePath}/Home/Impostazioni",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["Area"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["Area"].Value}", Link = $"{basePath}/Area/Create" }
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Area_Create"].Value}",
                    BackRoute = $"{basePath}/Area",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Area"].Value}", $"{basePath}/Area", $"{sharedLocalizer["Create"].Value}" })
                };

                return header;
            }

            public static HeaderClass AggiungiLingua(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["MA_AddLanguage"].Value}",
                    BackRoute = $"{basePath}/Area",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Area"].Value}", $"{basePath}/Area", $"{sharedLocalizer["MA_AddLanguage"].Value}" })
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Details"].Value}  {sharedLocalizer["Area"].Value}",
                    BackRoute = $"{basePath}/Area",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Area"].Value}", $"{basePath}/Area", $"{sharedLocalizer["Details"].Value}" })
                };
                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Area_Modifiy"].Value}",
                    BackRoute = $"{basePath}/Area",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Area"].Value}", $"{basePath}/Area", $"{sharedLocalizer["Edit"].Value}" })
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Area_Delete"].Value}",
                    BackRoute = $"{basePath}/Area",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Area"].Value}", $"{basePath}/Area", $"{sharedLocalizer["Delete"].Value}" })
                };

                return header;
            }

        }

    }
}
