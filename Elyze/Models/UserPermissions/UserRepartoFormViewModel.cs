// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.UserPermissions
{

    public class RepartoPermission
    {
        public int IdReparto { get; set; }
        public bool Selected { get; set; }
    }

    public class UserRepartoFormViewModel
    {
        public List<RepartoPermission> RepartoPermissions { get; set; } = new List<RepartoPermission>();
    }
}
