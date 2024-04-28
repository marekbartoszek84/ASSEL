namespace Assel.Contacts.Domain.Models
{
    public class ContactResponse
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid? CategoryId { get; set; }
        public CategoryResponse? Category { get; set; }
        public Guid? SubCategoryId { get; set; }
        public SubCategoryResponse? SubCategory { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
