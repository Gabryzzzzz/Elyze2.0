// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;
using Elyze.Models;

namespace Elyze.Data
{
    public class InserimentiFissi : EntityBase
    {
        public int Id { get; set; }

        [ForeignKey("IdUtenteNavigation")]
        public string IdUtente { get; set; }
        public virtual AspNetUsers IdUtenteNavigation { get; set; }

        [ForeignKey("IdValidatoreNavigation")]
        public string? IdValidatore { get; set; }
        public virtual AspNetUsers IdValidatoreNavigation { get; set; }


        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public DateTime? DataInserimento { get; set; }
        public DateTime? DataValidazione { get; set; }
        public int? Stato { get; set; }
        public int RepartoId { get; set; }
        public int SocietaId { get; set; }

        public int IdInserimento { get; set; }

        [ForeignKey("IdMicroAreaNavigation")]
        public int IdMicroArea { get; set; }
        public MicroArea IdMicroAreaNavigation { get; set; }



    }
}
