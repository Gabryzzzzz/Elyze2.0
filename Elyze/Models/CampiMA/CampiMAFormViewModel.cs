// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.CampiMA
{
    public class CampiMADataTableViewModel
    {
        //Id, IdMicroArea, Descrizione, InizioValidita, FineValidita, IdTipologia
        public int Id { get; set; }
        public int IdCampo { get; set; }

        public string MicroArea { get; set; } = string.Empty;
        public string Tipologia { get; set; } = string.Empty;
        public string UDM { get; set; } = string.Empty;

        public string Descrizione { get; set; } = string.Empty;
        public string DataCreazione { get; set; } = string.Empty;
        public string DataSpegnimento { get; set; } = string.Empty;

        public int LinguaId { get; set; } = 0;
        public string LinguaName { get; set; } = "";
    }
}
