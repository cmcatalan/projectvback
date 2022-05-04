using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Dtos.UserService;
using System.Security.Claims;

namespace ProjectVBack.Application.Services
{
    public interface IUserAppService
    {
        Task<string> LogIn(AuthenticateRequest request);
        Task<UserDto> SignUp(RegisterRequest request);
        Task<UserDto> GetUserInfoAsync(string userId);
        Task<UserDto> UpdateUserInfo(EditUserRequest request , string Id);
    }
}