// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Elyze.Models
{
    public class Log
    {
        [BindProperty, DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
