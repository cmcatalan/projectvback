namespace ProjectVBack.Application.Dtos
{
    public record AddTransactionRequest(
        string Description,
        double Value,
        DateTime Date,
        int CategoryId);
}