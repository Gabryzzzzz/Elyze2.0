// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Elyze.Models.OperatoriDiConversione
{
    public class OperatoreDiConversioneViewDto
    {
        public int Id { get; set; }

        [Required]
        public int UnitaDiMisuraPartenzaId { get; set; }

        public SelectList? UnitaDiMisuraPartenza { get; set; } = null;

        [Required]
        public int UnitaDiMisuraArrivoId { get; set; }

        public SelectList? UnitaDiMisuraArrivo { get; set; } = null;

        [Required]
        public string FattoreDiConversione { get; set; } = string.Empty;

        [Required]
        public int OperazioneId { get; set; }

        public SelectList? Operazione { get; set; } = null;

        public string UnitaDiMisuraPartenzaDecrizione { get; set; } = string.Empty;

        public string UnitaDiMisuraArrivoDecrizione { get; set; } = string.Empty;

        public string OperazioneDecrizione { get; set; } = string.Empty;
    }
}
