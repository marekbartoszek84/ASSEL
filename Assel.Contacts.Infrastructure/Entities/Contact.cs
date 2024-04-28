using System.ComponentModel.DataAnnotations.Schema;

namespace Assel.Contacts.Infrastructure.Entities
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        [ForeignKey(nameof(SubCategory))]
        public Guid? SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
