// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.MicroArea
{
    public class MicroAreaDataTableViewModel
    {
        public int Id { get; set; } = 0;
        public int IdMicroArea { get; set; } = 0;

        public bool Stato { get; set; } = false;
        public string Descrizione { get; set; } = "";
        public string DataCreazione { get; set; } = "";
        public string DataSpegnimento { get; set; } = "";

        public int AreaId { get; set; } = 0;
        public string AreaName { get; set; } = "";

        public int GriId { get; set; } = 0;
        public string GriName { get; set; } = "";

        public int LinguaId { get; set; } = 0;
        public string LinguaName { get; set; } = "";
    }
}
