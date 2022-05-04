using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Dtos.UserService;
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
                var userId = GetUserId();

                var userInfo = await _userAppService.GetUserInfoAsync(userId);

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

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(EditUserRequest request)
        {
            var userId = GetUserId();

            var isModified = await _userAppService.UpdateUserInfo(request , userId);

            return Ok(isModified);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<UserDto> users = new List<UserDto>();

            return Ok(users); // TODO: Call user service
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
    }
}
