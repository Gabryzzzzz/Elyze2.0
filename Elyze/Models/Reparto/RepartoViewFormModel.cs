// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Elyze.Models.Reparto
{

    public class RepartoViewFormModel
    {

        public int Id { get; set; } = 0;
        public string Nome { get; set; } = string.Empty;
        public int StabilimentoId { get; set; } = 0;
        public string Descrizione { get; set; } = string.Empty;

    }
}
