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
    }
}
