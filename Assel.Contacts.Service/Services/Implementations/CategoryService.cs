using Assel.Contacts.Domain.Entities;
using Assel.Contacts.Domain.Interfaces;
using Assel.Contacts.Domain.Models;
using AutoMapper;
using CSharpFunctionalExtensions;

namespace Assel.Contacts.Domain.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result> AddSubcategoryAsync(SubCategoryRequest subCategoryRequest)
        {
            var subCategory = _mapper.Map<SubCategory>(subCategoryRequest);

            await _categoryRepository.AddSubcategoryAsync(subCategory);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync()
        {
            var result = await _categoryRepository.GetAllAsync();

            return Result.Success(_mapper.Map<IEnumerable<CategoryResponse>>(result));
        }
    }
}
