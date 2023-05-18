// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public partial class Inserimenti : EntityBase
    {
        public int Id { get; set; }

        [ForeignKey("IdCampoNavigation")]
        public int IdCampo { get; set; }
        public virtual CampiMa IdCampoNavigation { get; set; }

        public string? ValoreCampo { get; set; }
        public string? ValoreValidato { get; set; } = null;
        public int IdInserimento { get; set; }
    }
}
