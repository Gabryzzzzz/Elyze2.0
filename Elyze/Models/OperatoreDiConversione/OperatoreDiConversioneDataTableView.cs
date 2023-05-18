// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.OperatoriDiConversione
{
    public class OperatoreDiConversioneDataTableView
    {
        public int Id { get; set; }
        public string FattoreDiConversione { get; set; } = string.Empty;
        public string UnitaDiMisuraPartenzaDecrizione { get; set; } = string.Empty;
        public string UnitaDiMisuraArrivoDecrizione { get; set; } = string.Empty;
        public string OperazioneDescrizione { get; set; } = string.Empty;
    }
}
