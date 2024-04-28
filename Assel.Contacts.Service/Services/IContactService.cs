using Assel.Contacts.Domain.Models;
using CSharpFunctionalExtensions;

namespace Assel.Contacts.Domain.Services
{
    public interface IContactService
    {
        Task<Result<IEnumerable<ContactListResponse>>> GetAllAsync();
        Task<Result<ContactResponse>> GetAsync(Guid id);
        Task<Result> AddAsync(ContactRequest contactRequest);
        Task<Result> UpdateAsync(Guid id, ContactRequest contact);
        Task<Result> DeleteAsync(Guid id);
    }
}
