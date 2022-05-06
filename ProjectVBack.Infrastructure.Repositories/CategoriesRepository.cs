using Microsoft.EntityFrameworkCore;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Infrastructure.Persistence;

namespace ProjectVBack.Infrastructure.Repositories
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly MoneyAppContext _context;
        public CategoriesRepository(MoneyAppContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<Category?> GetCategoryWithUsersByIdAsync(int id)
        {
            return await _context.Categories.Include(category => category.Users).FirstOrDefaultAsync(category => category.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(string userId)
        {
            return await _context.Categories
                .Include(category => category.Users)
                .Where(category => category.Users.Any(user => user.Id == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetDefaultCategoriesAsync()
        {
            return await _context.Categories
                .Where(category => category.IsDefault == true)
                .ToListAsync();
        }
    }
}
