using ProjectVBack.Domain.Entities;

namespace ProjectVBack.Domain.Repositories.Abstractions
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryWithUsersByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(string  id);
        Task<IEnumerable<Category>> GetDefaultCategoriesAsync();
    }
}