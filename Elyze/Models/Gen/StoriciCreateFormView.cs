// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.Gen
{
    public class CampiDinamici
    {
        public int Id { get; set; } = 0;
        //Label
        public string Descrizione { get; set; } = string.Empty;
        //Valore che inserisce l'utente
        public string Valore { get; set; } = string.Empty;
        //Tipo del campo
        public int IdTipo { get; set; } = 0;
        //Unita di misura
        public int IdUDM { get; set; } = 0;
    }

    public class CampiFissi
    {
        public int Id { get; set; } = 0;
        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public int? Stato { get; set; }
        public int RepartoId { get; set; } = 0;
        public int SocietaId { get; set; } = 0;
        public int StabilimentoId { get; set; } = 0;
        public int IdInserimento { get; set; } = 0;
        public int IdMicroArea { get; set; } = 0;
    }

    public class StoriciCreateFormView
    {
        public CampiFissi CampiFissi { get; set; } = new CampiFissi();
        public CampiDinamici[] CampiDinamici { get; set; }
    }
}
