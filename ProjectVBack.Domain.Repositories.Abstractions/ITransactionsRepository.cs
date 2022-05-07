using ProjectVBack.Domain.Entities;

namespace ProjectVBack.Domain.Repositories.Abstractions
{
    public interface ITransactionsRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetFiltered(string userId, DateTime? from, DateTime? to);
    }
}