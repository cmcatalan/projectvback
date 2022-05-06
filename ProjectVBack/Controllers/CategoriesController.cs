using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Services;
using System.Security.Claims;

namespace ProjectVBack.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryAppService _categoryAppService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryAppService categoryAppService)
        {
            _logger = logger;
            _categoryAppService = categoryAppService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCategory(AddCategoryRequest request)
        {
             var user = HttpContext.User;

            if (user == null)
                throw new Exception();

            var userId = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);

            if (userId == null)
                throw new Exception();

            var result = await _categoryAppService.CreateCategoryAsync(request , userId.Value);

            if (result == null)
                throw new Exception();

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var userId = GetUserId();

            var category = await _categoryAppService.GetCategoryAsync(categoryId , userId);

            return Ok(category);
        }

        private string GetUserId()
        {
            var user = HttpContext.User;

            if (user == null)
                throw new Exception();

            var userClaims = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);

            if (userClaims == null)
                throw new Exception();

            if (string.IsNullOrEmpty(userClaims.Value))
                throw new Exception();

            return userClaims.Value;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCategories()
        {
            var userId = GetUserId();

            var categoriesList = await _categoryAppService.GetAllCategoriesAsync(userId);

            return Ok(categoriesList);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditCategory(EditCategoryRequest request)
        {
            var userId = GetUserId();

            var categorieEdited = await _categoryAppService.EditCategoryAsync(request , userId);

            return Ok(categorieEdited);
        }

        [HttpDelete]
        [Authorize]
        [Route("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var userId = GetUserId();

            var deletedCategory = await _categoryAppService.DeleteCategoryAsync(categoryId , userId);
            
            return Ok(deletedCategory);
        }
    }
}