namespace Assel.Contacts.Infrastructure.Entities
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
