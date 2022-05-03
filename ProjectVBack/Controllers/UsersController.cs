using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Services;
using System.Security.Claims;

namespace ProjectVBack.WebApi.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserAppService userAppService, ILogger<UsersController> logger)
        {
            _userAppService = userAppService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var user = HttpContext.User;

                if (user == null)
                    return NotFound();

                var userClaims = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid);

                if(userClaims == null)
                    return NotFound();

                if (string.IsNullOrEmpty(userClaims.Value))
                    return NotFound();

                var userInfo = await _userAppService.GetUserInfoAsync(userClaims.Value);

                if (userInfo == null)
                    return NotFound();

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
