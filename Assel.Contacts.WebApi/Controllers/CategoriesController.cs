using Assel.Contacts.Domain.Models;
using Assel.Contacts.Domain.Services;
using Assel.Contacts.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assel.Contacts.WebApi.Controllers
{
    [Route("api/categories")]
    [Authorize]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var result = await _categoryService.GetAllAsync();

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAsync(SubCategoryRequest subCategoryRequest)
        {
            var result = await _categoryService.AddSubcategoryAsync(subCategoryRequest);

            return result.ToActionResult(Ok, errors => BadRequest(errors));
        }
    }
}
