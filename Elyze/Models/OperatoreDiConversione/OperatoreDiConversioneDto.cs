// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.OperatoriDiConversione
{
    public class OperatoreDiConversioneDto
    {
        public int Id { get; set; }
        public int? IdUnitaDiMisuraPartenza { get; set; }
        public int? IdUnitaDiMisuraArrivo { get; set; }
        public int? IdOperazione { get; set; }
        public decimal? FattoreDiConversione { get; set; }

        public string UnitaDiMisuraPartenzaDecrizione { get; set; } = string.Empty;
        public string UnitaDiMisuraArrivoDecrizione { get; set; } = string.Empty;
        public string OperazioneDecrizione { get; set; } = string.Empty;
    }
}
