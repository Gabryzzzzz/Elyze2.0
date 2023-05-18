// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public partial class Sede : EntityBase
    {

        public int Id { get; set; }
        public string? Descrizione { get; set; }
        public string? SedeN { get; set; }
        public string? IsoNazione { get; set; }
        public string? Nazione { get; set; }
        public bool? Attivo { get; set; }

        [ForeignKey("SocietaIdNavigation")]
        public int SocietaId { get; set; }
        public virtual Societa SocietaIdNavigation { get; set; }

    }
}
