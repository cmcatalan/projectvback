using ProjectVBack.Crosscutting.Utils;

namespace ProjectVBack.Application.Dtos
{
    public record GetTransactionsSummaryRequest(DateTime? From, DateTime? To);
}