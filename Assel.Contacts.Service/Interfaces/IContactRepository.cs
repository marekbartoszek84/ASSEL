using Assel.Contacts.Domain.Entities;

namespace Assel.Contacts.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetDetailsAsync(Guid id);
        Task<Contact> GetByEmailAsync(string email);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(Guid id);
    }
}
