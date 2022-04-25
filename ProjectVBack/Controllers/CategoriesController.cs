using Microsoft.AspNetCore.Mvc;

namespace ProjectVBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        //TODO Get all categories only by user logged, user_id from usermanager
        //TODO Get category by id only by user logged, user_id from usermanager
        //TODO Post category (type, name, image_url, description) user_id from usermanager
        //TODO Put category (id, type, name, image_url, description) user_id from usermanager
        //TODO Delete category (id) user_id from usermanager
    }
}