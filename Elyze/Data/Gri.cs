// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Models;

namespace Elyze.Data
{
    public partial class Gri : EntityBase
    {
        public int Id { get; set; }
        public string? CodiceGri { get; set; }
        public string? Titolo { get; set; }
        public bool Stato { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataSpegnimento { get; set; }
    }
}
