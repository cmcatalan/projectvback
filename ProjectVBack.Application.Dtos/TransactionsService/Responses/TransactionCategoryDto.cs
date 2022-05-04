namespace ProjectVBack.Application.Dtos
{
    public record TransactionCategoryDto(int TransactionId, string TransactionDescription, double TransactionValue, DateTime TransactionDate, string CategoryType, string CategoryName, string CategoryImageUrl, bool CategoryIsDefault);
}