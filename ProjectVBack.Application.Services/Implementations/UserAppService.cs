using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectVBack.Application.Services.Implementations
{
    public class UserAppService : IUserAppService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public UserAppService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> LogIn(AuthenticateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) as User;

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new Exception("Can't find user");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email)
              };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return jwt;
        }

        public async Task<bool> SignUp(RegisterRequest request)
        {
            var newUser = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            
            return result.Succeeded;
        }
    }
}
