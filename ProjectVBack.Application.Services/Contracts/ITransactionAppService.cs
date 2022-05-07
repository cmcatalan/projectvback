using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services
{
    public interface ITransactionAppService
    {
        Task<IEnumerable<TransactionCategoryDto>> GetAllTransactionsWithCategoryInfo(string userId, GetTransactionsRequest dto);
        Task<TransactionsSummaryDto> GetSummary(string userId, GetTransactionsSummaryRequest dto);
        Task<IEnumerable<TransactionsSumGroupByCategory>> GetTransactionsSumGroupByCategory(string userId, GetTransactionsRequest dto);
        Task<TransactionDto> Add(string userId, AddTransactionRequest dto);
        Task<TransactionDto> Edit(string userId, EditTransactionRequest dto);
        Task<TransactionDto> Delete(string userId, int transactionId);
    }
}