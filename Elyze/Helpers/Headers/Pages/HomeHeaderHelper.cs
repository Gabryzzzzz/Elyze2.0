// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class Home
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Dashboard"].Value
                };

                return header;
            }

            public static HeaderClass Gerarchie(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Gerarchie_Configurazione"].Value,
                    BackRoute = $"{basePath}/Home",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Gerarchie"].Value}" })
                };

                return header;
            }

            public static HeaderClass Impostazioni(IHtmlLocalizer<SharedResource> sharedLocalizer, string basePath)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Settings"].Value,
                    BackRoute = $"{basePath}/Home",
                    Breadcrumb = BreadClass.CreateBread(new string[] { $"{sharedLocalizer["Dashboard"].Value}", "/", $"{sharedLocalizer["Settings"].Value}" })
                };

                return header;
            }
        }

    }
}
