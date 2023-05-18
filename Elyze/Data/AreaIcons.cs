// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using Elyze.Models;

namespace Elyze.Data
{
    public class AreaIcons : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? Descrizione { get; set; }
        public string? Codice { get; set; }
        public string LocalPath { get; internal set; }
    }
}
