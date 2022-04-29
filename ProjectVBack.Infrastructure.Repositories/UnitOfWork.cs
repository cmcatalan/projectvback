using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Infrastructure.Persistence;

namespace ProjectVBack.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoneyAppContext _context;
        public ITransactionsRepository Transactions { get; }
        public ICategoriesRepository Categories { get; }

        public UnitOfWork(MoneyAppContext context,
            ITransactionsRepository transactionsRepository,
            ICategoriesRepository categoriesRepository)
        {
            _context = context;

            Transactions = transactionsRepository;
            Categories = categoriesRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
