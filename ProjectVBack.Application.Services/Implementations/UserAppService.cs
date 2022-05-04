using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Dtos.UserService;
using ProjectVBack.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectVBack.Application.Services
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

        public async Task<UserDto> SignUp(RegisterRequest request)
        {
            var newUser = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (result.Succeeded)
            {
                newUser = await _userManager.FindByEmailAsync(request.Email);

                if (newUser == null)
                    throw new Exception();

                UserDto newUserDto = new UserDto();

                newUserDto.FirstName = newUser.FirstName;
                newUserDto.LastName = newUser.LastName;
                newUserDto.UserName = newUser.UserName;
                newUserDto.Id = newUser.Id;
                newUserDto.Email = newUser.Email;   

                return newUserDto;
            }

            throw new Exception();
        }

        public async Task<UserDto> GetUserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) //Add custom ex user not found
                throw new Exception();

            UserDto userDto = new UserDto()
            {
                FirstName = user.FirstName,
                LastName= user.LastName,
                Email = user.Email,
                UserName = user.UserName,
            };

            return userDto;
        }

        public async Task<UserDto> UpdateUserInfo(EditUserRequest request , string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new Exception();

            string oldName = user.FirstName;
            string oldLastName = user.LastName;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var passwordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

                if (!passwordResult.Succeeded)
                {
                    user.FirstName = oldName;
                    user.LastName = oldLastName;

                    result = await _userManager.UpdateAsync(user);

                    if(!result.Succeeded)
                        throw new Exception();

                    var userUpdated = await GetUserInfoAsync(id);
                    return userUpdated;
                }
                else
                {
                    var userUpdated = await GetUserInfoAsync(id);
                    return userUpdated;
                }
            }

            throw new Exception();
        }
    }
}
