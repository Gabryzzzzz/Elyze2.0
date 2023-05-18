// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Identity;

namespace Elyze.Data
{
    public class AspNetUsers : IdentityUser
    {
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public bool? Attivo { get; set; } = true;
    }
}
