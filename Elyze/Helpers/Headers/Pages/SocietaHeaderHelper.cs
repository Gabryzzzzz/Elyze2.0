// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class Societa
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Company"].Value,
                    BackRoute = $"{basePath}/Home/Gerarchie",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", sharedLocalizer["Company"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["Company"].Value}", Link = $"{basePath}/Societa/Create" }
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Company_Add"].Value}",
                    BackRoute = $"{basePath}/Societa",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Company"].Value}", $"{basePath}/Societa", $"{sharedLocalizer["Create"].Value}" })
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Details"].Value}  {sharedLocalizer["Company"].Value}",
                    BackRoute = $"{basePath}/Societa",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Company"].Value}", $"{basePath}/Societa", $"{sharedLocalizer["Details"].Value}" })
                };
                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Edit"].Value} {sharedLocalizer["Company"].Value}",
                    BackRoute = $"{basePath}/Societa",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Company"].Value}", $"{basePath}/Societa", $"{sharedLocalizer["Edit"].Value}" })
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Delete"].Value} {sharedLocalizer["Company"].Value}",
                    BackRoute = $"{basePath}/Societa",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{@sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Company"].Value}", $"{basePath}/Societa", $"{sharedLocalizer["Delete"].Value}" })
                };

                return header;
            }

        }
    }
}
