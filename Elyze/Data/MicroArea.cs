// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public partial class MicroArea : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Column(Order = 2)]
        public string? NomeTabella { get; set; }
        public bool Stato { get; set; }
        public string? Descrizione { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataSpegnimento { get; set; }

        [ForeignKey("IdAreaNavigation")]
        public int IdArea { get; set; }
        public virtual Area? IdAreaNavigation { get; set; }

        public int IdMicroArea { get; set; }

        [ForeignKey("IdGriNavigation")]
        public int IdGri { get; set; }
        public virtual Gri? IdGriNavigation { get; set; }
    }
}
