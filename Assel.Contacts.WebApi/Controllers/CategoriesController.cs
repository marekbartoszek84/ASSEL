using Assel.Contacts.Infrastructure.Entities;
using Assel.Contacts.Infrastructure.Repository;
using Assel.Contacts.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assel.Contacts.WebApi.Controllers
{
    [Route("api/categories")]
    [Authorize]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var result = await _categoryRepository.GetAllAsync();

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAsync(SubCategoryRequest subCategoryRequest)
        {
            var subCategory = _mapper.Map<SubCategory>(subCategoryRequest);

            if (subCategory != null)
            {
                await _categoryRepository.AddSubcategoryAsync(subCategory);
            }

            return Ok();
        }
    }
}
