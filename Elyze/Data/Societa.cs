// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public partial class Societa : EntityBase
    {
        public int Id { get; set; }

        [Column(Order = 3)]
        [ForeignKey("GruppoIdNavigation")]
        public int GruppoId { get; set; }
        public virtual Gruppo GruppoIdNavigation { get; set; }

        public string? Descrizione { get; set; }

        public string? CodiceIsoNazione { get; set; }
        public string? Nazione { get; set; }

        public bool? Attivo { get; set; }

        public ICollection<Repository> Repository { get; set; }

    }
}
