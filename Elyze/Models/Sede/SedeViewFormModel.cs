// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.Sede
{

    public class SedeViewFormModel
    {
        public int Id { get; set; } = 0;
        public string Descrizione { get; set; } = string.Empty;
        public string SedeN { get; set; } = string.Empty;
        public string IsoNazione { get; set; } = string.Empty;
        public string Nazione { get; set; } = string.Empty;
        public int SocietaId { get; set; } = 0;
    }
}
