using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Services;

namespace ProjectVBack.WebApi.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
            try
            {
                var jwt = await _userAppService.LogIn(request);

                if (jwt == null)
                    return BadRequest();

                return Ok(new { AccessToken = jwt });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't log in");
                return BadRequest();
            }
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                var user = HttpContext.User;

                var claims = user.Claims.Select(s => new { s.Type, s.Value }).ToList();

                var userInfo = new
                {
                    Claims = claims,
                    user.Identity.Name,
                    user.Identity.IsAuthenticated,
                    user.Identity.AuthenticationType
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can't get user from HttpContext");
                return BadRequest();
            }

        }
    }
}
