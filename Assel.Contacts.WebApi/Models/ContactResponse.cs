using Assel.Contacts.Infrastructure.Entities;

namespace Assel.Contacts.WebApi.Models
{
    public class ContactResponse
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
