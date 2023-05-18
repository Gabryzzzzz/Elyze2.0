// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class MicroArea
        {

            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_MicroArea"].Value,
                    BackRoute = $"{basePath}/Impostazioni",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["MA_MicroArea"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["MA_MicroArea"].Value}", Link = $"{basePath}/MicroArea/Create" }
                };

                return header;
            }

            public static HeaderClass AggiungiLingua(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["MA_AddLanguage"].Value}",
                    BackRoute = $"{basePath}/MicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea", sharedLocalizer["MA_AddLanguage"].Value, }),
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_MicroArea"].Value,
                    BackRoute = $"{basePath}/MicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea", sharedLocalizer["Delete"].Value, }),
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_MicroArea"].Value,
                    BackRoute = $"{basePath}/MicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea", sharedLocalizer["Details"].Value }),
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_MicroArea"].Value,
                    BackRoute = $"{basePath}/MicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea", sharedLocalizer["Create"].Value }),
                };

                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_MicroArea"].Value,
                    BackRoute = $"{basePath}/MicroArea",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["MA_MicroArea"].Value, $"{basePath}/MicroArea", sharedLocalizer["Edit"].Value }),
                };

                return header;
            }

            public static HeaderClass Main(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["MA_MicroArea"].Value,
                    BackRoute = $"{basePath}/Home",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["MA_MicroArea"].Value}" })
                };

                return header;
            }

        }

    }
}
