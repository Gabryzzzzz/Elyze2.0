// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.Repository
{
    public class RepositoryDataTableViewModel
    {
        public int Id { get; set; } = 0;

        public int IdArea { get; set; } = 0;

        public string Titolo { get; set; } = string.Empty;

        public string Descrizione { get; set; } = string.Empty;

        public string Areadescrizione { get; set; } = string.Empty;
    }
}
