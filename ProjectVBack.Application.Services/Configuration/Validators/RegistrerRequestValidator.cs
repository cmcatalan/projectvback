using FluentValidation;
using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services.Configuration
{
    public class RegistrerRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegistrerRequestValidator()
        {
            RuleFor(RegisterRequest => RegisterRequest.FirstName)
                .NotEmpty().WithMessage("El nombre no puede estar vacío")
                .Length(1,50).WithMessage("El nombre debe tener mas de 1 y menos de 50 caracteres");
            RuleFor(RegisterRequest => RegisterRequest.LastName)
                .NotEmpty().WithMessage("El apellido no puede estar vacío")
                .Length(1,50).WithMessage("El apellido debe tener mas de 1 y menos de 50 caracteres");
            RuleFor(RegisterRequest => RegisterRequest.Email)
                .NotEmpty().WithMessage("El email no puede estar vacío")
                .Length(1, 75).WithMessage("El email debe tener mas de 1 y menos de 75 caracteres")
                .EmailAddress().WithMessage("Introduce un Email válido");
            RuleFor(RegisterRequest => RegisterRequest.UserName)
                .NotEmpty().WithMessage("El username no puede estar vacio")
                .Length(1, 50).WithMessage("El username tiene que tener entre 1 y 50 caracteres");
            RuleFor(RegisterRequest => RegisterRequest.Password)
                .NotEmpty().WithMessage("El password no puede estar vacio")
                .Length(1, 50).WithMessage("El password tiene que tener entre 1 y 50 caracteres");
        }
    }
}
