using FluentValidation;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.Utils.Errors;

namespace ProjectVBack.Application.Services.Configuration
{
    public class RegistrerRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegistrerRequestValidator()
        {
            RuleFor(RegisterRequest => RegisterRequest.FirstName)
                .NotEmpty().WithMessage(ValidationMessagesTexts.NameEmptyError)
                .Length(1,50).WithMessage(ValidationMessagesTexts.NameLengthError);
            RuleFor(RegisterRequest => RegisterRequest.LastName)
                .NotEmpty().WithMessage(ValidationMessagesTexts.LastNameEmptyError)
                .Length(1,50).WithMessage(ValidationMessagesTexts.LastNameLengthError);
            RuleFor(RegisterRequest => RegisterRequest.Email)
                .NotEmpty().WithMessage(ValidationMessagesTexts.EmailEmptyError)
                .Length(1, 75).WithMessage(ValidationMessagesTexts.EmailLengthError)
                .EmailAddress().WithMessage(ValidationMessagesTexts.EmailInvalidError);
            RuleFor(RegisterRequest => RegisterRequest.UserName)
                .NotEmpty().WithMessage(ValidationMessagesTexts.UserNameEmptyError)
                .Length(1, 50).WithMessage(ValidationMessagesTexts.UserNameLengthError);
            RuleFor(RegisterRequest => RegisterRequest.Password)
                .NotEmpty().WithMessage(ValidationMessagesTexts.PasswordEmptyError)
                .Length(1, 50).WithMessage(ValidationMessagesTexts.PasswordLengthError);
        }
    }
}
