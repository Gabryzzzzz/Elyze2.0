// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class Valuta
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Currency"].Value,
                    BackRoute = $"{basePath}/Home/Impostazioni",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", sharedLocalizer["Currency"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["Currency"].Value}", Link = $"{basePath}/Valuta/Create" }
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Currency_Add"].Value}",
                    BackRoute = $"{basePath}/Valuta",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Currency"].Value}", $"{basePath}/Valuta", $"{sharedLocalizer["Create"].Value}" })
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Details"].Value}  {sharedLocalizer["Currency"].Value}",
                    BackRoute = $"{basePath}/Valuta",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Currency"].Value}", $"{basePath}/Valuta", $"{sharedLocalizer["Details"].Value}" })
                };
                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Edit"].Value} {sharedLocalizer["Currency"].Value}",
                    BackRoute = $"{basePath}/Valuta",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Currency"].Value}", $"{basePath}/Valuta", $"{sharedLocalizer["Edit"].Value}" })
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Delete"].Value} {sharedLocalizer["Currency"].Value}",
                    BackRoute = $"{basePath}/Valuta",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}", $"{basePath}/Home/Impostazioni", $"{sharedLocalizer["Currency"].Value}", $"{basePath}/Valuta", $"{sharedLocalizer["Delete"].Value}" })
                };

                return header;
            }

        }
    }
}
