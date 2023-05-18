// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class User
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Users"].Value,
                    BackRoute = $"{basePath}/",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", sharedLocalizer["Users"].Value }),
                    HeaderAction = new HeaderAction { Name = $"{sharedLocalizer["Create"].Value} {sharedLocalizer["Users"].Value}", Link = $"{basePath}/Users/Create" }
                };

                return header;
            }

            public static HeaderClass Configurations(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Users"].Value,
                    BackRoute = $"{basePath}/Users",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", sharedLocalizer["Users"].Value, $"{basePath}/Users", $"{sharedLocalizer["User_Config"].Value}" }),
                };

                return header;
            }

            public static HeaderClass Create(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["User_New"].Value}",
                    BackRoute = $"{basePath}/Users",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Users"].Value}", $"{basePath}/Users", $"{sharedLocalizer["Create"].Value}" })
                };

                return header;
            }

            public static HeaderClass Details(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Details"].Value}  {sharedLocalizer["Users"].Value}",
                    BackRoute = $"{basePath}/Users",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Users"].Value}", $"{basePath}/Users", $"{sharedLocalizer["Details"].Value}" })
                };
                return header;
            }

            public static HeaderClass Edit(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Modify"].Value} {sharedLocalizer["Users"].Value}",
                    BackRoute = $"{basePath}/Users",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Users"].Value}", $"{basePath}/Users", $"{sharedLocalizer["Edit"].Value}" })
                };

                return header;
            }

            public static HeaderClass EditPassword(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Change_Pwd"].Value}",
                    BackRoute = $"{basePath}/Users",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Users"].Value}", $"{basePath}/Users", $"{sharedLocalizer["Change_Pwd"].Value}" })
                };

                return header;
            }

            public static HeaderClass Delete(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = $"{sharedLocalizer["Delete"].Value} {sharedLocalizer["Users"].Value}",
                    BackRoute = $"{basePath}/Users",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Users"].Value}", $"{basePath}/Users", $"{sharedLocalizer["Delete"].Value}" })
                };

                return header;
            }

        }

    }
}
