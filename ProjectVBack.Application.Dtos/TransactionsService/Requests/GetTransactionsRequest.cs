using ProjectVBack.Crosscutting.Utils;

namespace ProjectVBack.Application.Dtos
{
    public record GetTransactionsRequest(DateTime? From, DateTime? To, CategoryType? CategoryType);
}