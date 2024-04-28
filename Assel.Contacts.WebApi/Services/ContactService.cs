using Assel.Contacts.Repository.Entities;
using Assel.Contacts.Repository.Repository;
using Assel.Contacts.WebApi.Models;
using AutoMapper;
using CSharpFunctionalExtensions;

namespace Assel.Contacts.WebApi.Services
{
    public interface IContactService
    {
        Result<IEnumerable<ContactListResponse>> GetAll();
        Result<ContactResponse> GetDetails(Guid id);
        Result Add(ContactRequest contactRequest);
        Result Update(Guid id, ContactRequest contact);
        Result Delete(Guid id);
    }

    public class ContactService : IContactService
    {
        private static readonly string[] AllowedCategories = { "Sluzbowy", "Prywatny", "Inny" };

        private readonly ICategoryRepository _categoryRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Result Add(ContactRequest contactRequest)
        {
            var contact = _mapper.Map<Contact>(contactRequest);

            if (contact == null)
            {
                return Result.Failure(Errors.ContactRequestRequiredError);
            }

            if (!string.IsNullOrWhiteSpace(contactRequest.Email))
            {
                var existedContact = _contactRepository.GetByEmail(contactRequest.Email);

                if (existedContact != null)
                {
                    return Result.Failure(Errors.ContactAlreadyExistsError);
                }
            }

            if (contactRequest.CategoryId != null)
            {
                var category = _categoryRepository.Get(contactRequest.CategoryId.Value);
                
                if (category == null || !AllowedCategories.Contains(category.Name))
                {
                    return Result.Failure(Errors.InvalidCategoryIdError);
                }
            }

            if (!CheckCategoryId(contactRequest.CategoryId))
            {
                return Result.Failure(Errors.InvalidCategoryIdError);
            }

            if (!CheckSubCategory(contactRequest.CategoryId, contactRequest.SubCategoryId))
            {
                return Result.Failure(Errors.InvalidSubCategoryError);
            }

            _contactRepository.Add(contact);
            return Result.Success();
        }

        public Result Delete(Guid id)
        {
            var existedContact = _contactRepository.GetDetails(id);

            if (existedContact == null)
            {
                return Result.Failure(Errors.ContactDoesNotExistError);
            }

            _contactRepository.Delete(id);
            return Result.Success();
        }

        public Result<IEnumerable<ContactListResponse>> GetAll()
        {
            var contacts = _contactRepository.GetAll();

            return Result.Success(_mapper.Map<IEnumerable<ContactListResponse>>(contacts));
        }

        public Result<ContactResponse> GetDetails(Guid id)
        {
            var contact = _contactRepository.GetDetails(id);
            return Result.Success(_mapper.Map<ContactResponse>(contact));
        }

        public Result Update(Guid id, ContactRequest contactRequest)
        {
            var contact = _contactRepository.GetDetails(id);

            if (contact == null)
            {
                return Result.Failure(Errors.ContactDoesNotExistError);
            }

            if (contactRequest.Email != contact.Email && !string.IsNullOrWhiteSpace(contactRequest.Email) && contactRequest.Email.ToLower() != contact.Email.ToLower())
            {
                var existingContactEmail = _contactRepository.GetByEmail(contactRequest.Email);

                if (existingContactEmail != null)
                    return Result.Failure(Errors.ContactWithRequestedEmailExistsError);
            }

            if (!CheckCategoryId(contactRequest.CategoryId))
            {
                return Result.Failure(Errors.InvalidCategoryIdError);
            }

            if (!CheckSubCategory(contactRequest.CategoryId, contactRequest.SubCategoryId))
            {
                return Result.Failure(Errors.InvalidSubCategoryError);
            }

            _mapper.Map(contactRequest, contact);

            _contactRepository.Update(contact);
            return Result.Success();
        }

        private bool CheckCategoryId(Guid? categoryId)
        {
            if (categoryId != null)
            {
                var category = _categoryRepository.Get(categoryId.Value);

                if (category == null || !AllowedCategories.Contains(category.Name))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckSubCategory(Guid? categoryId, Guid? subCategoryId)
        {
            if (subCategoryId != null)
            {
                if (categoryId == null)
                    return false;

                var category = _categoryRepository.Get(categoryId.Value);

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
