using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Services;
using ProjectVBack.Crosscutting.CustomExceptions;
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
                throw new AppIGetMoneyUserNotFoundException();

            var userId = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);

            if (userId == null)
                throw new AppIGetMoneyException();

            var result = await _categoryAppService.CreateCategoryAsync(request , userId.Value);

            if (result == null)
                throw new AppIGetMoneyCategoryCreationException();

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var userId = GetUserId();

            var category = await _categoryAppService.GetCategoryAsync(categoryId , userId);

            if(category == null)
                throw new AppIGetMoneyCategroyNotFoundException();

            return Ok(category);
        }

        private string GetUserId()
        {
            var user = HttpContext.User;

            if (user == null)
                throw new AppIGetMoneyUserNotFoundException();

            var userClaims = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);

            if (string.IsNullOrEmpty(userClaims.Value))
                throw new AppIGetMoneyException();

            return userClaims.Value;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCategories()
        {
            var userId = GetUserId();

            var categoriesList = await _categoryAppService.GetAllCategoriesAsync(userId);

            if (!categoriesList.Any())
                throw new AppIGetMoneyException();

            return Ok(categoriesList);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditCategory(EditCategoryRequest request)
        {
            var userId = GetUserId();

            var categorieEdited = await _categoryAppService.EditCategoryAsync(request , userId);

            if (categorieEdited == null)
                throw new AppIGetMoneyCategroyNotFoundException();

            return Ok(categorieEdited);
        }

        [HttpDelete]
        [Authorize]
        [Route("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var userId = GetUserId();

            var deletedCategory = await _categoryAppService.DeleteCategoryAsync(categoryId , userId);

            if (deletedCategory == null)
                throw new AppIGetMoneyCategroyNotFoundException();
            
            return Ok(deletedCategory);
        }
    }
}