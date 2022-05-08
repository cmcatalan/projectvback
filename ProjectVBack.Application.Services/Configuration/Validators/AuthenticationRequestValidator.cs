using FluentValidation;
using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services.Configuration
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(AuthenticateRequest => AuthenticateRequest.UserName)
                .NotEmpty().WithMessage("El nombre no puede estar vacío")
                .Length(1,50).WithMessage("El nombre debe tener mas de 1 y menos de 50 caracteres");
            RuleFor(AuthenticateRequest => AuthenticateRequest.Password)
                .NotEmpty().WithMessage("La contraseña no puede estar vacía")
                .Length(1,50).WithMessage("La contraseña debe tener mas de 1 y menos de 50 caracteres");
        }
    }
}
