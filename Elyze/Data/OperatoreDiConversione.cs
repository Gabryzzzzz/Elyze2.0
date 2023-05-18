// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Models;

namespace Elyze.Data
{
    public partial class OperatoreDiConversione : EntityBase
    {

        //public OperatoreDiConversione()
        //{
        //    OperatoreDiConversioneIdOperazione = new HashSet<Operazione>();
        //}

        public int Id { get; set; }
        public int? IdUnitaDiMisuraPartenza { get; set; }
        public int? IdUnitaDiMisuraArrivo { get; set; }
        public int? IdOperazione { get; set; }
        public decimal? FattoreDiConversione { get; set; }

        public virtual Operazione? IdOperazioneNavigation { get; set; }
        public virtual UnitaMisura? IdUnitaDiMisuraArrivoNavigation { get; set; }
        public virtual UnitaMisura? IdUnitaDiMisuraPartenzaNavigation { get; set; }
    }
}
