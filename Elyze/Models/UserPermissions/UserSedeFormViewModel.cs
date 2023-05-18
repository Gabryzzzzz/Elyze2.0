// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.UserPermissions
{

    public class SedePermission
    {
        public int IdSede { get; set; }
        public bool Selected { get; set; }
    }

    public class UserSedeFormViewModel
    {
        public List<SedePermission> SedePermissions { get; set; } = new List<SedePermission>();
    }
}
