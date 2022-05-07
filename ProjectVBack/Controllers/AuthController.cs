using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Services;

namespace ProjectVBack.WebApi.Services.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserAppService userAppService, ILogger<AuthController> logger)
        {
            _userAppService = userAppService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(AuthenticateRequest request)
        {
            var jwt = await _userAppService.LogIn(request);

            return Ok(new { AccessToken = jwt });
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterRequest request)
        {
            try
            {
                var newUserDto = await _userAppService.SignUp(request);
                if (newUserDto == null)
                    return BadRequest();

                return Ok(newUserDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't sign up");
                return BadRequest();
            }
        }
    }
}
