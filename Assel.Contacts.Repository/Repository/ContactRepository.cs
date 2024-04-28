using Assel.Contacts.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assel.Contacts.Repository.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact>? GetAll();
        Contact? GetDetails(Guid id);
        Contact? GetByEmail(string email);
        void Add(Contact contact);
        void Update(Contact contact);
        void Delete(Guid id);
    }

    public class ContactRepository : IContactRepository
    {
        protected ContactDbContext context;

        public ContactRepository(ContactDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Contact>? GetAll()
        {
            var result = context.Contacts?.ToList();

            return result;
        }
        public Contact? GetByEmail(string email)
        {
            var result = context.Contacts?
                .Where(c => !string.IsNullOrWhiteSpace(c.Email) && c.Email.ToLower() == email.ToLower()).FirstOrDefault();

            return result;
        }

        public Contact? GetDetails(Guid id)
        {
            var result = context.Contacts?
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Where(c => c.Id == id).FirstOrDefault();

            return result;
        }

        public void Add(Contact contact)
        {
            context.Contacts?.Add(contact);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var contact = context.Contacts?
                .Where(c => c.Id == id).FirstOrDefault();

            if (contact != null)
            {
                context.Contacts?.Remove(contact);
                context.SaveChanges();
            }
        }

        public void Update(Contact contact)
        {
            context.Contacts?.Update(contact);
            context.SaveChanges();
        }
    }
}
