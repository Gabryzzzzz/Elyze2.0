// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Models;

namespace Elyze.Data
{
    public partial class Repository : EntityBase
    {
        public int Id { get; set; }
        public string? Titolo { get; set; }
        public string? Descrizione { get; set; }
        public string? FilePath { get; set; }
        public int AreaId { get; set; }
        public int SocietaId { get; set; }
        public int? AttachmentId { get; set; }

        public Societa Societa { get; set; }
        public Attachment? Attachment { get; set; }
    }
}
