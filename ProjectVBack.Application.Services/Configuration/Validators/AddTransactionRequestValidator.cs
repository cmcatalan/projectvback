using FluentValidation;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.Utils.Errors;

namespace ProjectVBack.Application.Services.Configuration
{
    public class AddTransactionRequestValidator : AbstractValidator<AddTransactionRequest>
    {
        public AddTransactionRequestValidator()
        {
            RuleFor(AddTransactionRequest => AddTransactionRequest.Description)
                .Length(1,200).WithMessage(ValidationMessagesTexts.DescriptionLengthError);
            RuleFor(AddTransactionRequest => AddTransactionRequest.Value)
                .NotEmpty().WithMessage(ValidationMessagesTexts.ValueEmptyError)
                .LessThan(10000000).WithMessage(ValidationMessagesTexts.ValueMaxValError);
            RuleFor(AddTransactionRequest => AddTransactionRequest.Date)
                .NotEmpty().WithMessage(ValidationMessagesTexts.DateEmptyError)
                .LessThan(c => DateTime.Now).WithMessage(ValidationMessagesTexts.DateValidError);
            RuleFor(AddTransactionRequest => AddTransactionRequest.CategoryId)
                .NotEmpty().WithMessage(ValidationMessagesTexts.CategoryIdEmptyError);
        }
    }
}
