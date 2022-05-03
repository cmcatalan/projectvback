using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Services;

namespace ProjectVBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryAppService _categoryAppService;

        public CategoriesController(ILogger<CategoriesController> logger , ICategoryAppService categoryAppService)
        {
            _logger = logger;
            _categoryAppService = categoryAppService;
        }



        //TODO Get all categories only by user logged, user_id from usermanager
        //TODO Get category by id only by user logged, user_id from usermanager
        //TODO Post category (type, name, image_url, description) user_id from usermanager
        //TODO Put category (id, type, name, image_url, description) user_id from usermanager
        //TODO Delete category (id) user_id from usermanager
    }
}