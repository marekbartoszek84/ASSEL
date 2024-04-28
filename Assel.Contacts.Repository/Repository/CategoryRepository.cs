using Assel.Contacts.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.Repository.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category>? GetAll();
        Category? Get(Guid id);
        void AddSubcategory(SubCategory subCategory);
    }

    public class CategoryRepository : ICategoryRepository
    {
        protected ContactDbContext context;

        public CategoryRepository(ContactDbContext _context)
        {
            context = _context;
        }

        public IEnumerable<Category>? GetAll()
        {
            var result = context.Categories?
                .Include(c => c.SubCategories)
                .ToList();

            return result;
        }

        public void AddSubcategory(SubCategory subCategory)
        {
            context.SubCategories?.Add(subCategory);
            context.SaveChanges();
        }

        public Category? Get(Guid id)
        {
            var result = context.Categories?
                .Include(c => c.SubCategories)
                .Where(c => c.Id == id).FirstOrDefault();

            return result;
        }
    }
}
