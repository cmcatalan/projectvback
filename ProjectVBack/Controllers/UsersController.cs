using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Services;
using ProjectVBack.Crosscutting.CustomExceptions;
using System.Security.Claims;

namespace ProjectVBack.WebApi.Services.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
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
            var userId = GetUserId();

            var userInfo = await _userAppService.GetUserInfoAsync(userId);

            if (userInfo == null)
                return NotFound();

            return Ok(userInfo);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(EditUserRequest request)
        {
            var userId = GetUserId();

            var isModified = await _userAppService.UpdateUserInfo(request, userId);

            if (isModified == null)
                throw new AppIGetMoneyUserNotFoundException();

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
            var claims = HttpContext.User.Claims;

            if (claims == null || !claims.Any())
                throw new AppIGetMoneyException(nameof(claims));

            var claim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

            if (claim == null || string.IsNullOrEmpty(claim.Value))
                throw new AppIGetMoneyException(nameof(claim));

            var userId = claim.Value;

            return userId;
        }
    }
}
