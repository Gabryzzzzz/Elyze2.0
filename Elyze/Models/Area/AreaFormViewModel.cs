// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.Area
{
    public class AreaFormViewModel
    {
        public int Id { get; set; }
        public int IdArea { get; set; }

        public string Descrizione { get; set; } = "";
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public DateTime DataSpegnimento { get; set; } = DateTime.Now;
        public bool Stato { get; set; } = false;
        public int IdIconaArea { get; set; }
        public int[]? IdsIconeSdg { get; set; }
        public int IdLingua { get; set; }
    }
}
