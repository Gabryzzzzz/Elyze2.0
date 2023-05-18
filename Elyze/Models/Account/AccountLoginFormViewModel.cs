using System.ComponentModel.DataAnnotations;

namespace Elyze.Models.Account
{
    public class AccountLoginFormViewModel
    {
        [Required]
        [StringLength(255)]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string? Password { get; set; }

        public bool IsAuthorized { get; set; }

        public bool IsTenantValid { get; set; }

        public bool UrlCompliance { get; set; }
    }
}
