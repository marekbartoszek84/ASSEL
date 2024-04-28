namespace Assel.Contacts.Domain.Models
{
    public class SubCategoryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
