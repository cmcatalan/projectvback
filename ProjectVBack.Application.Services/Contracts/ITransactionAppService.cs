using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services
{
    public interface ITransactionAppService
    {
        Task<IEnumerable<TransactionCategoryDto>> GetAll(int userId, GetTransactionsRequest dto);
        Task<TransactionsSummaryDto> GetSummary(int userId, GetTransactionsRequest dto);
        Task<IEnumerable<TransactionsSumGroupByCategory>> GetTransactionsSumGroupByCategory(int userId, GetTransactionsRequest dto);
        Task<IEnumerable<TransactionCategoryDto>> Add(int userId, AddTransactionRequest dto);
        Task<IEnumerable<TransactionCategoryDto>> Edit(int userId, EditTransactionRequest dto);
        Task<IEnumerable<TransactionCategoryDto>> Delete(int userId, int transactionId);
    }
}