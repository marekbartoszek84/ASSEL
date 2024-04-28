using Assel.Contacts.Domain.Models;
using CSharpFunctionalExtensions;

namespace Assel.Contacts.Domain.Services
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync();
        Task<Result> AddSubcategoryAsync(SubCategoryRequest subCategory);
    }
}
