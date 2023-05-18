// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Models;

namespace Elyze.Data
{
    public partial class Gruppo : EntityBase
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descrizione { get; set; }
        public bool? Attivo { get; set; }
    }
}
