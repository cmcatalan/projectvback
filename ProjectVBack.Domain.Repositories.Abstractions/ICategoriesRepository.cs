using ProjectVBack.Crosscutting.Utils;
using ProjectVBack.Domain.Entities;

namespace ProjectVBack.Domain.Repositories.Abstractions
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithUsersByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(string userId);
        Task<IEnumerable<Category>> GetDefaultCategoriesAsync();
        Task<IEnumerable<Category>> GetFiltered(IEnumerable<int> categoryIds, CategoryType? type = null);
    }
}