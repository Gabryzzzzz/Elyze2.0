// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Helpers.Headers
{
    public static partial class HeaderClassBuilders
    {
        public static class UserSede
        {
            public static HeaderClass Index(IHtmlLocalizer<SharedResource> sharedLocalizer)
            {
                HeaderClass header = new()
                {
                    Title = sharedLocalizer["Area"].Value + " " + sharedLocalizer["User"].Value
                };

                return header;
            }
        }
    }

}
