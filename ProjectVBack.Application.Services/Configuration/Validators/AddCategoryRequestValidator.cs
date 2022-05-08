using FluentValidation;
using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services.Configuration
{
    public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
    {
        public AddCategoryRequestValidator()
        {
            RuleFor(AddCategoryRequest => AddCategoryRequest.Name)
                .NotEmpty().WithMessage("El nombre no puede estar vacío")
                .Length(1,50).WithMessage("El nombre debe tener mas de 1 y menos de 50 caracteres");
            RuleFor(AddCategoryRequest => AddCategoryRequest.PictureUrl)
                .NotEmpty().WithMessage("Debe seleccionar una imagen");
            RuleFor(AddCategoryRequest => AddCategoryRequest.Description)
                .Length(1, 200).WithMessage("La descripción debe tener más de 1 y menos de 200 caracteres");
        }
    }
}
