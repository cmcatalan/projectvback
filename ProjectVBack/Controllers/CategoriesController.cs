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

        //TODO Get all categories only by user logged, user_id from usermanager
        //TODO Get category by id only by user logged, user_id from usermanager
        //TODO Post category (type, name, image_url, description) user_id from usermanager
        //TODO Put category (id, type, name, image_url, description) user_id from usermanager
        //TODO Delete category (id) user_id from usermanager
    }
}