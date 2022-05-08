using FluentValidation;
using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services.Configuration
{
    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(EditUserRequest => EditUserRequest.FirstName)
                .NotEmpty().WithMessage("El nombre no puede estar vacío")
                .Length(1,50).WithMessage("El nombre debe tener mas de 1 y menos de 50 caracteres");
            RuleFor(EditUserRequest => EditUserRequest.LastName)
                .NotEmpty().WithMessage("El apellido no puede estar vacío")
                .Length(1,50).WithMessage("El apellido debe tener mas de 1 y menos de 50 caracteres");
            RuleFor(EditUserRequest => EditUserRequest.OldPassword)
                .NotEmpty().WithMessage("La antigua contraseña no puede estar vacia");
            RuleFor(EditUserRequest => EditUserRequest.NewPassword)
                .NotEmpty().WithMessage("La nueva contraseña no puede estar vacia");
        }
    }
}
