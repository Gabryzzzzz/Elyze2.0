// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.CampiMA
{
    public class CampiMAFormViewModel
    {
        //Id, IdMicroArea, Descrizione, InizioValidita, FineValidita, IdTipologia
        public int Id { get; set; }
        public int IdMicroArea { get; set; }
        public int IdTipologia { get; set; }
        public int IdUDM { get; set; }
        public int IdCampoMA { get; set; }


        public string Descrizione { get; set; } = string.Empty;
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public DateTime DataSpegnimento { get; set; } = DateTime.Now.AddDays(1);
    }
}
