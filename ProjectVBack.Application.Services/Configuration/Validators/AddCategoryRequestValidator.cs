using FluentValidation;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.Utils.Errors;

namespace ProjectVBack.Application.Services.Configuration
{
    public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
    {
        public AddCategoryRequestValidator()
        {
            RuleFor(AddCategoryRequest => AddCategoryRequest.Name)
                .NotEmpty().WithMessage(ValidationMessagesTexts.NameEmptyError)
                .Length(1,50).WithMessage(ValidationMessagesTexts.NameLengthError);
            RuleFor(AddCategoryRequest => AddCategoryRequest.PictureUrl)
                .NotEmpty().WithMessage(ValidationMessagesTexts.PictureNonSelectedError);
            RuleFor(AddCategoryRequest => AddCategoryRequest.Description)
                .Length(1, 200).WithMessage(ValidationMessagesTexts.DescriptionLengthError);
        }
    }
}
