using Microsoft.EntityFrameworkCore;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Infrastructure.Persistence;

namespace ProjectVBack.Infrastructure.Repositories
{
    public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
    {
        public TransactionsRepository(MoneyAppContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Transaction>> GetFiltered(string userId, DateTime? from, DateTime? to)
        {
            var transactionsQuery = Query().Where(transaction => transaction.UserId == userId);

            if (from != null)
                transactionsQuery = transactionsQuery.Where(transaction => transaction.Date >= from);

            if (to != null)
                transactionsQuery = transactionsQuery.Where(transaction => transaction.Date <= to);

            return await transactionsQuery.ToListAsync();
        }
    }
}
