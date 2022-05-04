using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Infrastructure.Persistence;

namespace ProjectVBack.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MoneyAppContext _context;
        protected GenericRepository(MoneyAppContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(TEntity itemToAdd)
        {
            var addedItem = await _context.Set<TEntity>().AddAsync(itemToAdd);

            return addedItem.Entity;
        }

        public async Task AddRange(IEnumerable<TEntity> itemsToAdd)
        {
            await _context.Set<TEntity>().AddRangeAsync(itemsToAdd);
        }

        public async Task<TEntity> Get(int itemId)
        {
            return await _context.Set<TEntity>().FindAsync(itemId);
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> Update(TEntity itemToUpdate)
        {
            var row = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == itemToUpdate.Id);

            if (row == null) return null;

            itemToUpdate.UpdatedAt = DateTime.Now.ToUniversalTime();

            _context.Entry(itemToUpdate).State = EntityState.Modified;

            return itemToUpdate;
        }

        public async Task<TEntity> HardDelete(TEntity itemToDelete)
        {
            var deletedItem = await Task.Run(() => { return _context.Set<TEntity>().Remove(itemToDelete); });

            return deletedItem.Entity;
        }

        public async Task<TEntity> SoftDelete(TEntity itemToDelete)
        {
            var row = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == itemToDelete.Id);

            if (row == null) return null;

            itemToDelete.IsDeleted = true;
            itemToDelete.DeletedAt = DateTime.Now.ToUniversalTime();

            _context.Entry(itemToDelete).State = EntityState.Modified;

            return row;
        }
    }
}