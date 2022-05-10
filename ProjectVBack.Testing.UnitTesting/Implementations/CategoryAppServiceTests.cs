using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectVBack.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using FluentValidation;
using ProjectVBack.Application.Dtos;
using System.Security.Claims;
using ProjectVBack.Application.Dtos.CategoryService;
using ProjectVBack.Crosscutting.Utils;
using ProjectVBack.Crosscutting.CustomExceptions;
using System.Threading;
using FluentValidation.Internal;

namespace ProjectVBack.Application.Services.Tests
{
    [TestClass()]
    public class CategoryAppServiceTests
    {
        private readonly Mock<IUserStore<User>> _userStoreMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<EditCategoryRequest>> _editCategoryRequestValidatorMock;
        private readonly Mock<IValidator<AddCategoryRequest>> _addCategoryRequestValidatorMock;

        public CategoryAppServiceTests()
        {
            _userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);
            _userManagerMock.Object.UserValidators.Add(new UserValidator<User>());
            _userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<User>());


            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(f => f.Categories.AddAsync(It.IsAny<Category>()))
                .Returns(Task.FromResult(FixtureDataCategory.category));
            _unitOfWorkMock.Setup(f => f.Complete()).Verifiable();

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(f => f.Map<Category>(It.IsAny<CategoryDto>()))
                .Returns(FixtureDataCategory.category);
            _mapperMock.Setup(f => f.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(FixtureDataCategory.categoryDto);
            _mapperMock.Setup(f => f.Map<Category>(It.IsAny<AddCategoryRequest>()))
                .Returns(FixtureDataCategory.category);




            _editCategoryRequestValidatorMock = new Mock<IValidator<EditCategoryRequest>>();
            _editCategoryRequestValidatorMock.Setup(x => x.ValidateAsync(It.IsAny<EditCategoryRequest>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(FixtureDataCategory.validationResult);

            _addCategoryRequestValidatorMock = new Mock<IValidator<AddCategoryRequest>>();
            _addCategoryRequestValidatorMock.Setup(x => x.ValidateAsync(It.IsAny<AddCategoryRequest>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(FixtureDataCategory.validationResult);
        }

        [TestMethod()]
        public void CategoryAppServiceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task Create_Category_Async_Test_Happy_Path()
        {
            _unitOfWorkMock.Setup(f => f.Categories.AddAsync(It.IsAny<Category>())).Returns(Task.FromResult(FixtureDataCategory.category));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.userId)).Returns(Task.FromResult(FixtureDataCategory.user));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = await categoryAppService.CreateCategoryAsync(FixtureDataCategory.addCategoryRequest, FixtureDataCategory.userId);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(CategoryDto));
        }

        [TestMethod]
        [ExpectedException(typeof(AppIGetMoneyUserNotFoundException))]
        public async Task Create_Category_Async_Test_BadPath_User_Not_Found()
        {
            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

           await categoryAppService.CreateCategoryAsync(FixtureDataCategory.addCategoryRequest, FixtureDataCategory.userId);
        }

        [TestMethod()]
        public void GetCategoryAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllCategoriesAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task EditCategoryAsyncTest()
        {
            Category categoryTest = new Category()
            {
                Type = CategoryType.Expense,
                Name = "Travelling",
                PictureUrl = "thispictureUrl",
                Description = "This category is for the traveling expended money",
                IsDefault = false,
                Users = new List<User>()
                {
                    FixtureDataCategory.user
                },
                Transactions = new List<Transaction>(),
            };

            _unitOfWorkMock.Setup(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.categoryId))
                .Returns(Task.FromResult((Category ?)categoryTest));
            _unitOfWorkMock.Setup(f => f.Categories.UpdateAsync(FixtureDataCategory.category))
                .Returns(Task.FromResult((Category?)FixtureDataCategory.category));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.userId))
                .Returns(Task.FromResult(FixtureDataCategory.user));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = await categoryAppService.EditCategoryAsync(FixtureDataCategory.editCategoryRequest , FixtureDataCategory.userId);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(CategoryDto));
        }

        [TestMethod()]
        public void DeleteCategoryAsyncTest()
        {
            Assert.Fail();
        }
    }
}