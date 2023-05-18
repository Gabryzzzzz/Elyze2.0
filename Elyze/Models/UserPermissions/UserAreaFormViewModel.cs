// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.UserPermissions
{

    public class AreaPermission
    {
        public int IdArea { get; set; }
        public bool Selected { get; set; }
        public int IdType { get; set; }
    }

    public class UserAreaFormViewModel
    {
        //list of AreaPermission
        public List<AreaPermission> AreaPermissions { get; set; } = new List<AreaPermission>();
    }
}
