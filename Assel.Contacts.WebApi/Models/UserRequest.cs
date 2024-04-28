using System.ComponentModel.DataAnnotations;

namespace Assel.Contacts.WebApi.Models
{
    public class UserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password don't match.")]
        public string? ConfirmPassword { get; set; }
    }

}
