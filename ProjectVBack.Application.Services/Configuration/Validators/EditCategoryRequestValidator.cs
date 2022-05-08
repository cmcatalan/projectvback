using FluentValidation;
using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services.Configuration
{
    public class EditCategoryRequestValidator : AbstractValidator<EditCategoryRequest>
    {
        public EditCategoryRequestValidator()
        {
            RuleFor(EditCategoryRequest => EditCategoryRequest.Name)
                .NotEmpty().WithMessage("El nombre no puede estar vacío")
                .Length(1,50).WithMessage("El nombre debe tener mas de 1 y menos de 50 caracteres");
            RuleFor(EditCategoryRequest => EditCategoryRequest.PictureUrl)
                .NotEmpty().WithMessage("Debe seleccionar una imagen");
            RuleFor(EditCategoryRequest => EditCategoryRequest.Description)
                .Length(1, 200).WithMessage("La descripción debe tener más de 1 y menos de 200 caracteres");
        }
    }
}
