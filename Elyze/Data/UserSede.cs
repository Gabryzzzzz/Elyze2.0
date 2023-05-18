// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public partial class UserSede : EntityBase
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("IdSedeNavigation")]
        public int IdSede { get; set; }
        public virtual Sede IdSedeNavigation { get; set; } = null!;

        [ForeignKey("IdUserNavigation")]
        public string IdUser { get; set; }
        public virtual AspNetUsers IdUserNavigation { get; set; } = null!; public string? UserType { get; set; }
    }
}
