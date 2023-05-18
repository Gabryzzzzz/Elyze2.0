// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public partial class CampiMa : EntityBase
    {
        public int Id { get; set; }
        public string? Descrizione { get; set; }

        [ForeignKey("IdMicroAreaNavigation")]
        public int IdMicroArea { get; set; }
        public virtual MicroArea IdMicroAreaNavigation { get; set; }

        [ForeignKey("IdTipologiaNavigation")]
        public int IdTipologia { get; set; }
        public virtual TipologieCampiMicroArea IdTipologiaNavigation { get; set; }

        [ForeignKey("IdUDMNavigation")]
        public int IdUDM { get; set; }
        public virtual UnitaMisura IdUDMNavigation { get; set; }

        public int IdCampoMa { get; set; }

        public DateTime InizioValidita { get; set; }
        public DateTime FineValidita { get; set; }
    }
}
