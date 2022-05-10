using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.CustomExceptions;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using ProjectVBack.Application.Services.Configuration;
using AutoMapper;

namespace ProjectVBack.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<EditUserRequest> _editUserRequestValidator;
        private readonly IValidator<RegisterRequest> _registerRequestValidator;
        private readonly IValidator<AuthenticateRequest> _authenticateRequestValidator;
        private readonly IMapper _mapper;

        public UserAppService(IConfiguration configuration, UserManager<User> userManager, IUnitOfWork unitOfWork,
            IValidator<EditUserRequest> editUserRequestvalidator, IValidator<RegisterRequest> registerRequestValidator,
            IValidator<AuthenticateRequest> authenticateRequestValidator, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _editUserRequestValidator = editUserRequestvalidator;
            _registerRequestValidator = registerRequestValidator;
            _authenticateRequestValidator = authenticateRequestValidator;
            _mapper = mapper;
        }

        public async Task<string> LogIn(AuthenticateRequest request)
        {
            var validationResult = _authenticateRequestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                if (validationResult.Errors.Any())
                    throw new AppIGetMoneyInvalidUserException(validationResult.Errors[0].ErrorMessage);

                throw new AppIGetMoneyInvalidUserException();
            }

            User? user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new AppIGetMoneyUserNotFoundException();

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email)
              };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var jwtIssuer = Environment.GetEnvironmentVariable("JwtIssuer");
            var jwtAudience = Environment.GetEnvironmentVariable("JwtAudience");
            var jwtKey = Environment.GetEnvironmentVariable("JwtKey");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(720),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return jwt;
        }

        public async Task<UserDto> SignUp(RegisterRequest request)
        {
            var validationResult = _registerRequestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                if (validationResult.Errors.Any())
                {
                    throw new AppIGetMoneyInvalidUserException(validationResult.Errors[0].ErrorMessage);
                }

                throw new AppIGetMoneyInvalidUserException();
            }

            var defaultCategories = await _unitOfWork.Categories.GetDefaultCategoriesAsync();

            var newUser = _mapper.Map<User>(request);

            newUser.Categories = defaultCategories.ToList();

            var result = await _userManager.CreateAsync(newUser, request.Password);

            _unitOfWork.Complete();

            if (result.Succeeded)
            {
                newUser = await _userManager.FindByEmailAsync(request.Email);

                if (newUser == null)
                    throw new AppIGetMoneyUserNotFoundException();

                var newUserDto = _mapper.Map<UserDto>(request);

                return newUserDto;
            }

            throw new AppIGetMoneyException();
        }

        public async Task<UserDto> GetUserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new AppIGetMoneyUserNotFoundException();

            UserDto userDto = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
            };

            return userDto;
        }

        public async Task<UserDto> UpdateUserInfo(EditUserRequest request, string id)
        {
            var validationResult = _editUserRequestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                if(validationResult.Errors.Any())
                    throw new AppIGetMoneyInvalidUserException(validationResult.Errors[0].ErrorMessage);

                throw new AppIGetMoneyInvalidUserException();
            }
                
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new AppIGetMoneyUserNotFoundException();

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

                    if (!result.Succeeded)
                        throw new AppIGetMoneyException();

                    var userUpdated = await GetUserInfoAsync(id);
                    return userUpdated;
                }
                else
                {
                    var userUpdated = await GetUserInfoAsync(id);
                    return userUpdated;
                }
            }

            throw new AppIGetMoneyException();
        }
    }
}
