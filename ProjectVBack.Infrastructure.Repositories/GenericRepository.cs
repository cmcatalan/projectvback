using Microsoft.EntityFrameworkCore;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace ProjectVBack.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MoneyAppContext _context;
        protected GenericRepository(MoneyAppContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity itemToAdd)
        {
            var addedItem = await _context.Set<TEntity>().AddAsync(itemToAdd);

            return addedItem.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> itemsToAdd)
        {
            await _context.Set<TEntity>().AddRangeAsync(itemsToAdd);
        }

        public async Task<TEntity?> GetAsync(int itemId)
        {
            return await _context.Set<TEntity>().FindAsync(itemId);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>().AsQueryable<TEntity>();
        }

        public async Task<TEntity?> UpdateAsync(TEntity itemToUpdate)
        {
            var row = await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == itemToUpdate.Id);

            if (row == null) return null;

            itemToUpdate.UpdatedAt = DateTime.Now.ToUniversalTime();

            _context.Entry(itemToUpdate).State = EntityState.Modified;

            return itemToUpdate;
        }

        public async Task<TEntity?> SoftDeleteAsync(TEntity itemToDelete)
        {
            var row = await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == itemToDelete.Id);

            if (row == null) return null;

            itemToDelete.IsDeleted = true;
            itemToDelete.DeletedAt = DateTime.Now.ToUniversalTime();
            
            _context.Entry(itemToDelete).State = EntityState.Modified;

            return row;
        }

        public TEntity HardDelete(TEntity itemToDelete)
        {
            var deletedItem = _context.Set<TEntity>().Remove(itemToDelete);

            return deletedItem.Entity;
        }
    }
}