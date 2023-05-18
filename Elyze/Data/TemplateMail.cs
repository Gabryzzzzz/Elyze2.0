// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Models;

namespace Elyze.Data
{
    public class TemplateMail : EntityBase
    {
        public int Id { get; set; }

        public int TipoMailId { get; set; }

        public string TemplateOggetto { get; set; }

        public string TemplateCorpo { get; set; }

        public TipologiaMail TipologiaMail { get; set; }
    }
}
