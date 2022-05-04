namespace ProjectVBack.Application.Dtos
{
    public record TransactionsSumGroupByCategory(
        int CategoryId,
        string CategoryType,
        string CategoryName,
        string CategoryPictureUrl,
        bool CategoryIsDefault,
        double TransactionsSum);
}