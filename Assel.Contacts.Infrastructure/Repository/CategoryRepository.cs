using Assel.Contacts.Domain.Entities;
using Assel.Contacts.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        protected ContactDbContext context;

        public CategoryRepository(ContactDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var result = await context.Categories
                .Include(c => c.SubCategories)
                .ToListAsync();

            return result;
        }

        public async Task AddSubcategoryAsync(SubCategory subCategory)
        {
            await context.SubCategories.AddAsync(subCategory);
            await context.SaveChangesAsync();
        }

        public async Task<Category?> GetAsync(Guid id)
        {
            var result = await context.Categories
                .Include(c => c.SubCategories)
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            return result;
        }
    }
}
