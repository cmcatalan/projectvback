using ProjectVBack.Application.Dtos;
using ProjectVBack.Domain.Entities;

namespace ProjectVBack.Application.Services
{
    public interface ICategoryAppService
    {
        Task<Category> CreateCategoryAsync(AddCategoryRequest request, string userId);
    }
}