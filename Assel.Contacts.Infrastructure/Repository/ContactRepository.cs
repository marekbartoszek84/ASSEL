using Assel.Contacts.Domain.Entities;
using Assel.Contacts.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.Infrastructure.Repository
{
    public class ContactRepository : IContactRepository
    {
        protected ContactDbContext context;

        public ContactRepository(ContactDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var result = await context.Contacts.ToListAsync();

            return result;
        }

        public async Task<Contact> GetByEmailAsync(string email)
        {
            var result = await context.Contacts
                .Where(c => !string.IsNullOrWhiteSpace(c.Email) && c.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Contact> GetDetailsAsync(Guid id)
        {
            var result = context.Contacts?
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Where(c => c.Id == id).FirstOrDefault();

            return result;
        }

        public async Task AddAsync(Contact contact)
        {
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var contact = await context.Contacts
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            if (contact != null)
            {
                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Contact contact)
        {
            context.Contacts.Update(contact);
            await context.SaveChangesAsync();
        }
    }
}
