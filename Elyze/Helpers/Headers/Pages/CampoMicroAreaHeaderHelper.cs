// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{

    public static partial class HeaderClassBuilders
    {
        public class CampoMicroArea
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_Fields"].Value,
                    BackRoute = $"{basePath}/Home/Impostazioni",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Impostazioni"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["MA_Fields"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["MA_Fields"].Value}", Link = $"{basePath}/CampiMicroArea/Create" }
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["MA_Fields"].Value}",
                    BackRoute = $"{basePath}/CampiMicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Impostazioni"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["MA_Fields"].Value}", $"{basePath}/CampiMicroArea", $"{sharedLocalizer["Create"].Value}" })
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Details"].Value}  {sharedLocalizer["MA_Fields"].Value}",
                    BackRoute = $"{basePath}/CampiMicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Impostazioni"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["MA_Fields"].Value}", $"{basePath}/CampiMicroArea", $"{sharedLocalizer["Details"].Value}" })
                };
                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Edit"].Value} {sharedLocalizer["MA_Fields"].Value}",
                    BackRoute = $"{basePath}/CampiMicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Impostazioni"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["MA_Fields"].Value}", $"{basePath}/CampiMicroArea", $"{sharedLocalizer["Edit"].Value}" })
                };

                return header;
            }

            public static HeaderClass AggiungiLingua(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Edit"].Value} {sharedLocalizer["MA_Fields"].Value}",
                    BackRoute = $"{basePath}/CampiMicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Impostazioni"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["MA_Fields"].Value}", $"{basePath}/CampiMicroArea", $"{sharedLocalizer["MA_AddLanguage"].Value}" })
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Delete"].Value} {sharedLocalizer["MA_Fields"].Value}",
                    BackRoute = $"{basePath}/CampiMicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Impostazioni"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["MA_Fields"].Value}", $"{basePath}/CampiMicroArea", $"{sharedLocalizer["Delete"].Value}" })
                };

                return header;
            }
        }
    }
}
