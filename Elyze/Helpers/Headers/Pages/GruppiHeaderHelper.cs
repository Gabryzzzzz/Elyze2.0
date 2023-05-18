﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class Gruppo
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Groups"].Value,
                    BackRoute = $"{basePath}/Home/Gerarchie",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", sharedLocalizer["Groups"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["Groups"].Value}", Link = $"{basePath}/Gruppo/Create" }
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Group_AddGroup"].Value}",
                    BackRoute = $"{basePath}/Gruppo",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Groups"].Value}", $"{basePath}/Gruppo", $"{sharedLocalizer["Create"].Value}" })
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Details"].Value}  {sharedLocalizer["Groups"].Value}",
                    BackRoute = $"{basePath}/Gruppo",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Groups"].Value}", $"{basePath}/Gruppo", $"{sharedLocalizer["Details"].Value}" })
                };
                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Edit"].Value} {sharedLocalizer["Groups"].Value}",
                    BackRoute = $"{basePath}/Gruppo",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Groups"].Value}", $"{basePath}/Gruppo", $"{sharedLocalizer["Edit"].Value}" })
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Delete"].Value} {sharedLocalizer["Groups"].Value}",
                    BackRoute = $"{basePath}/Gruppo",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}", $"{basePath}/Home/Gerarchie", $"{sharedLocalizer["Groups"].Value}", $"{basePath}/Gruppo", $"{sharedLocalizer["Delete"].Value}" })
                };

                return header;
            }

        }

    }
}
