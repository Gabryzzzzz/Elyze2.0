﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Models;

namespace Elyze.Data
{
    public class TipologiaMail : EntityBase
    {
        public int Id { get; set; }

        public string Descrizione { get; set; }

        public ICollection<TemplateMail> TemplateMails { get; set; }
    }
}
