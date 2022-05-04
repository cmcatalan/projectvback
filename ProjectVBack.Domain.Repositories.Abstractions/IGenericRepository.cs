using ProjectVBack.Domain.Entities;
using System.Linq.Expressions;

namespace ProjectVBack.Domain.Repositories.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity itemToAdd);
        Task AddRangeAsync(IEnumerable<TEntity> itemsToAdd);
        Task<TEntity?> GetAsync(int itemId);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Query();
        Task<TEntity?> UpdateAsync(TEntity itemToUpdate);
        Task<TEntity?> SoftDeleteAsync(TEntity itemToDelete);
        TEntity HardDelete(TEntity itemToDelete);
    }
}
