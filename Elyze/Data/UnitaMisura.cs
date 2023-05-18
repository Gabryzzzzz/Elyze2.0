// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Models;

namespace Elyze.Data
{
    public partial class UnitaMisura : EntityBase
    {
        public UnitaMisura()
        {
            OperatoreDiConversioneIdUnitaDiMisuraArrivoNavigations = new HashSet<OperatoreDiConversione>();
            OperatoreDiConversioneIdUnitaDiMisuraPartenzaNavigations = new HashSet<OperatoreDiConversione>();
        }

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Simbolo { get; set; }

        public virtual ICollection<OperatoreDiConversione> OperatoreDiConversioneIdUnitaDiMisuraArrivoNavigations { get; set; }
        public virtual ICollection<OperatoreDiConversione> OperatoreDiConversioneIdUnitaDiMisuraPartenzaNavigations { get; set; }
    }
}
