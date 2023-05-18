// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace Elyze.Models.Repository
{
    public class RepositoryFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Titolo { get; set; } = string.Empty;

        [Required]
        public string Descrizione { get; set; } = string.Empty;

        [Required]
        public int AreaId { get; set; }

        public SelectList? Area { get; set; }

        [Required]
        public int SocietaId { get; set; }

        public SelectList? Societa { get; set; }

        [Required]
        public IFormFile UploadedFiles { get; set; }
    }
}
