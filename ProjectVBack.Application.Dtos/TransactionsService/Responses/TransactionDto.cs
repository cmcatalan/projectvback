namespace ProjectVBack.Application.Dtos
{
    public record TransactionDto(
        int Id,
        string Description,
        double Value,
        DateTime Date,
        int CategoryId);
}