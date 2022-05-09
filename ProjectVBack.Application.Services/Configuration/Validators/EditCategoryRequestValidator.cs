using FluentValidation;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.Utils.Errors;

namespace ProjectVBack.Application.Services.Configuration
{
    public class EditCategoryRequestValidator : AbstractValidator<EditCategoryRequest>
    {
        public EditCategoryRequestValidator()
        {
            RuleFor(EditCategoryRequest => EditCategoryRequest.Name)
                .NotEmpty().WithMessage(ValidationMessagesTexts.NameEmptyError)
                .Length(1,50).WithMessage(ValidationMessagesTexts.NameLengthError);
            RuleFor(EditCategoryRequest => EditCategoryRequest.PictureUrl)
                .NotEmpty().WithMessage(ValidationMessagesTexts.PictureNonSelectedError);
            RuleFor(EditCategoryRequest => EditCategoryRequest.Description)
                .Length(1, 200).WithMessage(ValidationMessagesTexts.DescriptionLengthError);
        }
    }
}
