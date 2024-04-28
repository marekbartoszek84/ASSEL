using System.ComponentModel.DataAnnotations;

namespace Assel.Contacts.Domain.Models
{
    public class UserAuthenticationRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
