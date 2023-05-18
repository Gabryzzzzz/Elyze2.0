// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Models;

namespace Elyze.Data
{
    public class Attachment : EntityBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileBase64String { get; set; }

        public ICollection<Repository> Repository { get; set; }
    }
}
