using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Dtos.CategoryService;
using ProjectVBack.Crosscutting.CustomExceptions;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;

namespace ProjectVBack.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryAppService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateCategoryAsync(AddCategoryRequest request, string userId)
        {
            var actualUser = await _userManager.FindByIdAsync(userId);

            var categoryToAdd = _mapper.Map<Category>(request);

            categoryToAdd.Users.Add(actualUser);

            var result = await _unitOfWork.Categories.AddAsync(categoryToAdd);

            _unitOfWork.Complete();

            var categoryResult = _mapper.Map<CategoryDto>(result);

            return categoryResult;
        }

        public async Task<CategoryDto> GetCategoryAsync(int categoryId, string userId)
        {
            var actualUser = await _userManager.FindByIdAsync(userId);

            var category = await _unitOfWork.Categories.GetCategoryWithUsersByIdAsync(categoryId);

            if (category == null)
                throw new AppIGetMoneyCategroyNotFoundException();

            if (category.Users.Contains(actualUser))
            {
                var categoryDto = _mapper.Map<CategoryDto>(category);

                return categoryDto;
            }

            throw new AppIGetMoneyException();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(string userId)
        {
            var categoriesList = await _unitOfWork.Categories.GetAllCategoriesAsync(userId);

            if (categoriesList == null)
                throw new AppIGetMoneyCategroyNotFoundException();

            var categoriesDtoList = _mapper.Map<IEnumerable<CategoryDto>>(categoriesList);

            return categoriesDtoList;
        }

        public async Task<CategoryDto> EditCategoryAsync(EditCategoryRequest request, string userId)
        {
            var categoryToEdit = await _unitOfWork.Categories.GetCategoryWithUsersByIdAsync(request.Id);

            if (categoryToEdit == null)
                throw new AppIGetMoneyCategroyNotFoundException();

            var actualUser = await _userManager.FindByIdAsync(userId);

            if (actualUser == null)
                throw new AppIGetMoneyUserNotFoundException();

            if (!categoryToEdit.IsDefault && categoryToEdit.Users.Contains(actualUser))
            {
                categoryToEdit.Name = request.Name;
                categoryToEdit.PictureUrl = request.PictureUrl;
                categoryToEdit.Description = request.Description;
                categoryToEdit.Type = request.Type;

                var categoryEdited = await _unitOfWork.Categories.UpdateAsync(categoryToEdit);

                _unitOfWork.Complete();

                var categoryEditedDto = _mapper.Map<CategoryDto>(categoryEdited);

                return categoryEditedDto;
            }

            throw new AppIGetMoneyException();
        }

        public async Task<CategoryDto> DeleteCategoryAsync(int categoryId, string userId)
        {
            var actualUser = await _userManager.FindByIdAsync(userId);

            if (actualUser == null)
                throw new AppIGetMoneyUserNotFoundException();

            var categoryToDelete = await _unitOfWork.Categories.GetCategoryWithUsersByIdAsync(categoryId);

            if (categoryToDelete == null)
                throw new AppIGetMoneyCategroyNotFoundException();

            if (!categoryToDelete.Users.Contains(actualUser))
                throw new AppIGetMoneyException();

            if (categoryToDelete.IsDefault)
            {
                var result = actualUser.Categories.Remove(categoryToDelete);

                if (!result)
                    throw new AppIGetMoneyUserNotFoundException();

                _unitOfWork.Complete();

                var categoryDeleted = _mapper.Map<CategoryDto>(categoryToDelete);

                return categoryDeleted;
            }

            else
            {
                var categoryDeleted = _unitOfWork.Categories.HardDelete(categoryToDelete);

                if (categoryDeleted == null)
                    throw new AppIGetMoneyCategroyNotFoundException();

                _unitOfWork.Complete();

                var categoryDeletedDto = _mapper.Map<CategoryDto>(categoryToDelete);

                return categoryDeletedDto;
            }

        }
    }
}
