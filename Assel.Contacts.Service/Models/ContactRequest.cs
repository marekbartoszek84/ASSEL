using System.ComponentModel.DataAnnotations;

namespace Assel.Contacts.Domain.Models
{
    public class ContactRequest
    {
        [Required(ErrorMessage = "FirstName is required.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(7)]
        public string? Password { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SubCategoryRequest? SubCategoryRequest { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
