namespace ProjectVBack.Application.Dtos
{
    public record EditTransactionRequest(
        int Id,
        string Description,
        double Value,
        DateTime Date,
        int CategoryId);
}