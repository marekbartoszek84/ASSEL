using Assel.Contacts.Domain.Entities;
using Assel.Contacts.Domain.Interfaces;
using Assel.Contacts.Domain.Models;
using AutoMapper;
using CSharpFunctionalExtensions;

namespace Assel.Contacts.Domain.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result> AddAsync(ContactRequest contactRequest)
        {
            var contact = _mapper.Map<Contact>(contactRequest);

            if (!string.IsNullOrWhiteSpace(contactRequest.Email))
            {
                var existedContact = await _contactRepository.GetByEmailAsync(contactRequest.Email);

                if (existedContact != null)
                {
                    return Result.Failure(Errors.ContactAlreadyExistsError);
                }
            }

            if (contactRequest.CategoryId != null)
            {
                var category = await _categoryRepository.GetAsync(contactRequest.CategoryId.Value);
                
                if (category == null)
                {
                    return Result.Failure(Errors.InvalidCategoryIdError);
                }
            }

            if (!CheckCategoryId(contactRequest.CategoryId).Result)
            {
                return Result.Failure(Errors.InvalidCategoryIdError);
            }

            if (!CheckSubCategory(contactRequest.CategoryId, contactRequest.SubCategoryId).Result)
            {
                return Result.Failure(Errors.InvalidSubCategoryError);
            }

            await _contactRepository.AddAsync(contact);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var existedContact = _contactRepository.GetDetailsAsync(id);

            if (existedContact == null)
            {
                return Result.Failure(Errors.ContactDoesNotExistError);
            }

            await _contactRepository.DeleteAsync(id);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<ContactListResponse>>> GetAllAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<ContactListResponse>>(contacts));
        }

        public async Task<Result<ContactResponse>> GetAsync(Guid id)
        {
            var contact = await _contactRepository.GetDetailsAsync(id);
            return Result.Success(_mapper.Map<ContactResponse>(contact));
        }

        public async Task<Result> UpdateAsync(Guid id, ContactRequest contactRequest)
        {
            var contact = await _contactRepository.GetDetailsAsync(id);

            if (contact == null)
            {
                return Result.Failure(Errors.ContactDoesNotExistError);
            }

            if (contactRequest.Email != contact.Email && !string.IsNullOrWhiteSpace(contactRequest.Email) && contactRequest.Email.ToLower() != contact.Email.ToLower())
            {
                var existingContactEmail = await _contactRepository.GetByEmailAsync(contactRequest.Email);

                if (existingContactEmail != null)
                    return Result.Failure(Errors.ContactWithRequestedEmailExistsError);
            }

            if (!CheckCategoryId(contactRequest.CategoryId).Result)
            {
                return Result.Failure(Errors.InvalidCategoryIdError);
            }

            if (!CheckSubCategory(contactRequest.CategoryId, contactRequest.SubCategoryId).Result)
            {
                return Result.Failure(Errors.InvalidSubCategoryError);
            }

            _mapper.Map(contactRequest, contact);

            await _contactRepository.UpdateAsync(contact);
            return Result.Success();
        }

        private async Task<bool> CheckCategoryId(Guid? categoryId)
        {
            if (categoryId != null)
            {
                var category = await _categoryRepository.GetAsync(categoryId.Value);

                if (category == null)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> CheckSubCategory(Guid? categoryId, Guid? subCategoryId)
        {
            if (subCategoryId != null)
            {
                if (categoryId == null)
                    return false;

                var category = await _categoryRepository.GetAsync(categoryId.Value);

                if (category == null || !category.SubCategories.Any(s => s.Id == subCategoryId.Value))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public static class Errors
    {
        public static readonly string ContactRequestRequiredError = "ContactRequest is required";
        public static readonly string ContactAlreadyExistsError = "Contact already exists";
        public static readonly string ContactDoesNotExistError = "Contact does not exist";
        public static readonly string ContactWithRequestedEmailExistsError = "Contact with requested email exists";
        public static readonly string InvalidCategoryIdError = "Invalid category";
        public static readonly string InvalidSubCategoryError = "Cannot add sub category";
    }
}
