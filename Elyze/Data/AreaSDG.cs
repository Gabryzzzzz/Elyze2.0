// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public partial class AreaSDG : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdAreaNavigation")]
        public int IdArea { get; set; }
        public virtual Area? IdAreaNavigation { get; set; }

        [ForeignKey("IdSdgNavigation")]
        public int IdSdg { get; set; }
        public virtual SDG? IdSdgNavigation { get; set; }
    }
}
