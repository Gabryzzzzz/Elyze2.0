// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class UserArea
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath, string id)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["User_Area"].Value,
                    BackRoute = $"{basePath}/Users/Configurations/{id}",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", sharedLocalizer["Users"].Value, $"{basePath}/Users", $"{sharedLocalizer["User_Config"].Value}", $"{basePath}/Users/Configurations/{id}", $"{sharedLocalizer["User_Area"].Value}" }),
                };

                return header;
            }
        }
    }

}
