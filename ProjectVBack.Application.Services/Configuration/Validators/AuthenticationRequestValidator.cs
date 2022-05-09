using FluentValidation;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.Utils.Errors;

namespace ProjectVBack.Application.Services.Configuration
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(AuthenticateRequest => AuthenticateRequest.UserName)
                .NotEmpty().WithMessage(ValidationMessagesTexts.NameEmptyError)
                .Length(1,50).WithMessage(ValidationMessagesTexts.NameLengthError);
            RuleFor(AuthenticateRequest => AuthenticateRequest.Password)
                .NotEmpty().WithMessage(ValidationMessagesTexts.PasswordEmptyError)
                .Length(1,50).WithMessage(ValidationMessagesTexts.PasswordLengthError);
        }
    }
}
