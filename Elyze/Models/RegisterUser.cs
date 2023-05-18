using System.ComponentModel.DataAnnotations;

namespace Elyze.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
