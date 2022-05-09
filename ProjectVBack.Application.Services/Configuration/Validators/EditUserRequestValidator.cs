using FluentValidation;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.Utils.Errors;

namespace ProjectVBack.Application.Services.Configuration
{
    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(EditUserRequest => EditUserRequest.FirstName)
                .NotEmpty().WithMessage(ValidationMessagesTexts.NameEmptyError)
                .Length(1,50).WithMessage(ValidationMessagesTexts.NameLengthError);
            RuleFor(EditUserRequest => EditUserRequest.LastName)
                .NotEmpty().WithMessage(ValidationMessagesTexts.LastNameEmptyError)
                .Length(1,50).WithMessage(ValidationMessagesTexts.LastNameLengthError);
            RuleFor(EditUserRequest => EditUserRequest.OldPassword)
                .NotEmpty().WithMessage(ValidationMessagesTexts.OldPasswordEmptyError)
                .Length(1, 50).WithMessage(ValidationMessagesTexts.OldPasswordLengthError);
            RuleFor(EditUserRequest => EditUserRequest.NewPassword)
                .NotEmpty().WithMessage(ValidationMessagesTexts.NewPasswordEmptyError)
                .Length(1, 50).WithMessage(ValidationMessagesTexts.NewPasswordLengthError);
        }
    }
}
