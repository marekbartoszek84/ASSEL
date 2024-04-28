using Assel.Contacts.Domain.Entities;

namespace Assel.Contacts.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetAsync(Guid id);
        Task AddSubcategoryAsync(SubCategory subCategory);
    }
}
