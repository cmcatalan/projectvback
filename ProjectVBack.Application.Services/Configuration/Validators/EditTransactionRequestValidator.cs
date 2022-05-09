using FluentValidation;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Crosscutting.Utils.Errors;

namespace ProjectVBack.Application.Services.Configuration
{
    public class EditTransactionRequestValidator : AbstractValidator<EditTransactionRequest>
    {
        public EditTransactionRequestValidator()
        {
            RuleFor(EditTransactionRequest => EditTransactionRequest.Id)
                .NotEmpty().WithMessage(ValidationMessagesTexts.IdEmptyError);
            RuleFor(EditTransactionRequest => EditTransactionRequest.Description)
                .Length(1,200).WithMessage(ValidationMessagesTexts.DescriptionLengthError);
            RuleFor(EditTransactionRequest => EditTransactionRequest.Value)
                .NotEmpty().WithMessage(ValidationMessagesTexts.ValueEmptyError)
                .LessThan(10000000).WithMessage(ValidationMessagesTexts.ValueMaxValError);
            RuleFor(EditTransactionRequest => EditTransactionRequest.Date)
                .NotEmpty().WithMessage(ValidationMessagesTexts.DateEmptyError)
                .LessThan(c => DateTime.Now).WithMessage(ValidationMessagesTexts.DateValidError);
            RuleFor(EditTransactionRequest => EditTransactionRequest.CategoryId)
                .NotEmpty().WithMessage(ValidationMessagesTexts.CategoryIdEmptyError);
        }
    }
}
