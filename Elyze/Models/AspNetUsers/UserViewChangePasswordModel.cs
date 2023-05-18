// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;

namespace Elyze.Models.AspNetUsers
{
    public class UserViewChangePasswordModel
    {
        public string Id { get; set; } = "";

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [Required]
        //Regular expression of asp net identity
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
