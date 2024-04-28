namespace Assel.Contacts.Domain.Models
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<SubCategoryResponse>? SubCategories { get; set; }
        public bool IsOwnSubcategoryAllowed { get; set; }
    }
}
