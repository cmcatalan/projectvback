using ProjectVBack.Domain.Entities;
using System.Linq.Expressions;

namespace ProjectVBack.Domain.Repositories.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity itemToAdd);
        Task<TEntity> Get(int itemId);
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Update(TEntity itemToUpdate);
        Task<TEntity> SoftDelete(TEntity itemToDelete);
        Task<TEntity> HardDelete(TEntity itemToDelete);
    }
}
