using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Application.Dtos.CategoryService;
using Microsoft.AspNetCore.Identity;
using ProjectVBack.Application.Dtos;

namespace ProjectVBack.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryAppService(IUnitOfWork unitOfWork , UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Category> CreateCategoryAsync(AddCategoryRequest request , string userId) //TODO: Change to Dto
        {
            var actualUser = await _userManager.FindByIdAsync(userId);

            Category categoryToAdd = new Category()
            {
                Name = request.Name,
                PictureUrl = request.PictureUrl,
                Description = request.Description,
                IsDefault = false,
            };

            categoryToAdd.Users.Add(actualUser);

            var result = await _unitOfWork.Categories.Add(categoryToAdd);
            _unitOfWork.Complete();

            return categoryToAdd;
        }
    }
}
