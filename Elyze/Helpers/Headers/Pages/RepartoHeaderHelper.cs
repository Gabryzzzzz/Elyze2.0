// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class Reparto
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Rep"].Value,
                    BackRoute = $"{basePath}/Home/Gerarchie",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", sharedLocalizer["Rep"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["Rep"].Value}", Link = $"{basePath}/Reparto/Create" }
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Rep_Add"].Value}",
                    BackRoute = $"{basePath}/Reparto",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Rep"].Value}", $"{basePath}/Reparto", $"{sharedLocalizer["Create"].Value}" })
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Rep_Details"].Value}",
                    BackRoute = $"{basePath}/Reparto",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Rep"].Value}", $"{basePath}/Reparto", $"{sharedLocalizer["Details"].Value}" })
                };
                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Rep_Modify"].Value}",
                    BackRoute = $"{basePath}/Reparto",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Rep"].Value}", $"{basePath}/Reparto", $"{sharedLocalizer["Edit"].Value}" })
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Rep_Delete"].Value}",
                    BackRoute = $"{basePath}/Reparto",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Rep"].Value}", $"{basePath}/Reparto", $"{sharedLocalizer["Delete"].Value}" })
                };

                return header;
            }

        }
    }
}
