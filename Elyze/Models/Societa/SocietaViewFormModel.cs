// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.Societa
{
    public class SocietaViewFormModel
    {
        public int Id { get; set; } = 0;
        public string Descrizione { get; set; } = string.Empty;
        public string CodiceIsoNazione { get; set; } = string.Empty;
        public string Nazione { get; set; } = string.Empty;
        public int GruppoId { get; set; } = 0;
        public string GruppoName { get; set; } = string.Empty;

    }
}
