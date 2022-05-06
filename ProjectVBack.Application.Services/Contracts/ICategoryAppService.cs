using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Dtos.CategoryService;

namespace ProjectVBack.Application.Services
{
    public interface ICategoryAppService
    {
        Task<CategoryDto> CreateCategoryAsync(AddCategoryRequest request, string userId);
        Task<CategoryDto> GetCategoryAsync(int categoryId, string userId);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(string userId);
        Task<CategoryDto> EditCategoryAsync(EditCategoryRequest request , string userId);
        Task<CategoryDto> DeleteCategoryAsync(int categoryId , string userId);
    }
}