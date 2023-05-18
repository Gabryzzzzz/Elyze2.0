// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.Area
{
    public class AreaViewDataTableModel
    {
        public int Id { get; set; }
        public int IdArea { get; set; }
        public string? Descrizione { get; set; }
        public string? DataCreazione { get; set; }
        public string? DataSpegnimento { get; set; }
        public bool Stato { get; set; }
        public int? IdLingua { get; set; }
        public string? Lingua { get; set; }
    }
}
