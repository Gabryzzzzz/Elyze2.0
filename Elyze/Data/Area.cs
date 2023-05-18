// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Data
{
    public partial class Area : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? Descrizione { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataSpegnimento { get; set; }

        [Comment("Attiva o non attiva")]
        public bool Stato { get; set; } = false;

        [Comment("Con 'IdLingua' forma la chiave composta della tabella")]
        public int IdArea { get; set; }

        [ForeignKey("IdLinguaNavigation")]
        public int IdLingua { get; set; }
        public virtual Lingue? IdLinguaNavigation { get; set; }

        [Comment("Le aree hanno una sola icona, quindi la relazione è uno a uno")]
        [ForeignKey("IdIconaAreaNavigation")]
        public int IdIconaArea { get; set; }
        public virtual AreaIcons? IdIconaAreaNavigation { get; set; }

        //Non troviamo qua le icone svg perchè adesso esiste la tabella di
        //supporto "AreaSdg" con relazione uno a molti

    }



}
