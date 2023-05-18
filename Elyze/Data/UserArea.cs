// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public partial class UserArea : EntityBase
    {
        public int Id { get; set; }

        [ForeignKey("IdUserNavigation")]
        public string IdUser { get; set; }
        public virtual AspNetUsers IdUserNavigation { get; set; } = null!;

        [ForeignKey("IdAreaNavigation")]
        public int IdArea { get; set; }
        public virtual Area IdAreaNavigation { get; set; } = null!;

        [ForeignKey("IdUserTypeNavigation")]
        public int IdUserType { get; set; }
        public virtual UserTypesArea IdUserTypeNavigation { get; set; } = null!;

    }
}
