using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services
{
    public interface IUserAppService
    {
        Task<string> LogIn(AuthenticateRequest request);
        Task<bool> SignUp(RegisterRequest request);
    }
}