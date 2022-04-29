namespace ProjectVBack.Domain.Repositories.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriesRepository Categories { get; }
        ITransactionsRepository Transactions { get; }
        int Complete();
    }
}