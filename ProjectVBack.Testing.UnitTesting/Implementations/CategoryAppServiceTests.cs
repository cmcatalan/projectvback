﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                .Returns(Task.FromResult(FixtureDataCategory.Category));
            _unitOfWorkMock.Setup(f => f.Complete()).Verifiable();

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(f => f.Map<Category>(It.IsAny<CategoryDto>()))
                .Returns(FixtureDataCategory.Category);
            _mapperMock.Setup(f => f.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(FixtureDataCategory.CategoryDto);
            _mapperMock.Setup(f => f.Map<Category>(It.IsAny<AddCategoryRequest>()))
                .Returns(FixtureDataCategory.Category);




            _editCategoryRequestValidatorMock = new Mock<IValidator<EditCategoryRequest>>();
            _editCategoryRequestValidatorMock.Setup(x => x.ValidateAsync(It.IsAny<EditCategoryRequest>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(FixtureDataCategory.ValidationResult);

            _addCategoryRequestValidatorMock = new Mock<IValidator<AddCategoryRequest>>();
            _addCategoryRequestValidatorMock.Setup(x => x.ValidateAsync(It.IsAny<AddCategoryRequest>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(FixtureDataCategory.ValidationResult);
        }

        [TestMethod()]
        public void CategoryAppServiceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task Create_Category_Async_Test_Happy_Path()
        {
            _unitOfWorkMock.Setup(f => f.Categories.AddAsync(It.IsAny<Category>()))
                .Returns(Task.FromResult(FixtureDataCategory.Category));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.UserId))
                .Returns(Task.FromResult(FixtureDataCategory.User));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = await categoryAppService.CreateCategoryAsync(FixtureDataCategory.AddCategoryRequest, FixtureDataCategory.UserId);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(CategoryDto));
        }

        [TestMethod]
        [ExpectedException(typeof(AppIGetMoneyUserNotFoundException))]
        public async Task Create_Category_Async_Test_BadPath_User_Not_Found()
        {
            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

           await categoryAppService.CreateCategoryAsync(FixtureDataCategory.AddCategoryRequest, FixtureDataCategory.UserId);
        }

        [TestMethod()]
        public void GetCategoryAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task Get_All_Categories_Async_Test_HappyPath()
        {
            _unitOfWorkMock.Setup(f => f.Categories.GetAllCategoriesAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(FixtureDataCategory.CategoryList));
            _mapperMock.Setup(f => f.Map<IEnumerable<CategoryDto>>(It.IsAny<List<Category>>()))
                .Returns(FixtureDataCategory.CategoryDtoList);

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = await categoryAppService.GetAllCategoriesAsync(FixtureDataCategory.UserId);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(IEnumerable<CategoryDto>));

            _unitOfWorkMock.Verify(f => f.Categories.GetAllCategoriesAsync(FixtureDataCategory.UserId), Times.Once);
            _mapperMock.Verify(f => f.Map<IEnumerable<CategoryDto>>(It.IsAny<List<Category>>()), Times.Once);
        }

        [TestMethod()]
        public async Task Edit_Category_Async_Test_HappyPath()
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
                    FixtureDataCategory.User
                },
                Transactions = new List<Transaction>(),
            };

            _unitOfWorkMock.Setup(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId))
                .Returns(Task.FromResult((Category ?)categoryTest));
            _unitOfWorkMock.Setup(f => f.Categories.UpdateAsync(FixtureDataCategory.Category))
                .Returns(Task.FromResult((Category?)FixtureDataCategory.Category));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.UserId))
                .Returns(Task.FromResult(FixtureDataCategory.User));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = await categoryAppService.EditCategoryAsync(FixtureDataCategory.EditCategoryRequest , FixtureDataCategory.UserId);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(CategoryDto));
        }

        [TestMethod]
        [ExpectedException(typeof(AppIGetMoneyCategoryDefaultException))]
        public async Task Edit_Category_Async_Test_BadPath_Category_Is_Default()
        {
            Category categoryTest = new Category()
            {
                Type = CategoryType.Expense,
                Name = "Travelling",
                PictureUrl = "thispictureUrl",
                Description = "This category is for the traveling expended money",
                IsDefault = true,
                Users = new List<User>()
                {
                    FixtureDataCategory.User
                },
                Transactions = new List<Transaction>(),
            };

            _unitOfWorkMock.Setup(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId))
                .Returns(Task.FromResult((Category?)categoryTest));
            _unitOfWorkMock.Setup(f => f.Categories.UpdateAsync(FixtureDataCategory.Category))
                .Returns(Task.FromResult((Category?)FixtureDataCategory.Category));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.UserId))
                .Returns(Task.FromResult(FixtureDataCategory.User));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            await categoryAppService.EditCategoryAsync(FixtureDataCategory.EditCategoryRequest, FixtureDataCategory.UserId);
        }

        [TestMethod()]
        [ExpectedException(typeof(AppIGetMoneyInvalidUserException))]
        public async Task Edit_Category_Async_Test_BadPath_User_Doesnt_Contain_Category()
        {
            Category categoryTest = new Category()
            {
                Type = CategoryType.Expense,
                Name = "Travelling",
                PictureUrl = "thispictureUrl",
                Description = "This category is for the traveling expended money",
                IsDefault = false,
                Users = new List<User>(),
                Transactions = new List<Transaction>(),
            };

            _unitOfWorkMock.Setup(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId))
                .Returns(Task.FromResult((Category?)categoryTest));
            _unitOfWorkMock.Setup(f => f.Categories.UpdateAsync(FixtureDataCategory.Category))
                .Returns(Task.FromResult((Category?)FixtureDataCategory.Category));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.UserId))
                .Returns(Task.FromResult(FixtureDataCategory.User));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            await categoryAppService.EditCategoryAsync(FixtureDataCategory.EditCategoryRequest, FixtureDataCategory.UserId);
        }

        [TestMethod()]
        [ExpectedException(typeof(AppIGetMoneyUserNotFoundException))]
        public async Task Edit_Category_Async_Test_BadPath_User_Not_Found_Category()
        {
            Category categoryTest = new Category()
            {
                Type = CategoryType.Expense,
                Name = "Travelling",
                PictureUrl = "thispictureUrl",
                Description = "This category is for the traveling expended money",
                IsDefault = false,
                Users = new List<User>(),
                Transactions = new List<Transaction>(),
            };

            _unitOfWorkMock.Setup(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId))
                .Returns(Task.FromResult((Category?)categoryTest));
            _unitOfWorkMock.Setup(f => f.Categories.UpdateAsync(FixtureDataCategory.Category))
                .Returns(Task.FromResult((Category?)FixtureDataCategory.Category));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            await categoryAppService.EditCategoryAsync(FixtureDataCategory.EditCategoryRequest, FixtureDataCategory.UserId);
        }

        [TestMethod()]
        public async Task Delete_Category_Async_Test_HappyPath_If_Default_Category()
        {
            User userTest = new User();

            Category categoryTest = new Category()
            {
                Type = CategoryType.Expense,
                Name = "Travelling",
                PictureUrl = "thispictureUrl",
                Description = "This category is for the traveling expended money",
                IsDefault = true,
                Users = new List<User>()
                {
                    userTest
                },
                Transactions = new List<Transaction>(),
            };

            userTest.FirstName = "Guillermo";
            userTest.LastName = "Cuenca";
            userTest.Categories = new List<Category>()
            {
                categoryTest
            };
            userTest.Transactions = new List<Transaction>();

            _unitOfWorkMock.Setup(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId))
                .Returns(Task.FromResult((Category?)categoryTest));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.UserId))
                .Returns(Task.FromResult(userTest));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = await categoryAppService.DeleteCategoryAsync(FixtureDataCategory.CategoryId , FixtureDataCategory.UserId);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(CategoryDto));

            _unitOfWorkMock.Verify(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId), Times.Once);
            _userManagerMock.Verify(f => f.FindByIdAsync(FixtureDataCategory.UserId), Times.Once);
            _mapperMock.Verify(f => f.Map<CategoryDto>(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod()]
        public async Task Delete_Category_Async_Test_HappyPath_If_Not_Default_Category()
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
                    FixtureDataCategory.User,
                },
                Transactions = new List<Transaction>(),
            };

            _unitOfWorkMock.Setup(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId))
                .Returns(Task.FromResult((Category?)categoryTest));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.UserId))
                .Returns(Task.FromResult(FixtureDataCategory.User));
            _unitOfWorkMock.Setup(f => f.Categories.HardDelete(categoryTest))
                .Returns(categoryTest);

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = await categoryAppService.DeleteCategoryAsync(FixtureDataCategory.CategoryId, FixtureDataCategory.UserId);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(CategoryDto));

            _unitOfWorkMock.Verify(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId), Times.Once);
            _userManagerMock.Verify(f => f.FindByIdAsync(FixtureDataCategory.UserId), Times.Once);
            _unitOfWorkMock.Verify(f => f.Categories.HardDelete(categoryTest), Times.Once);
            _mapperMock.Verify(f => f.Map<CategoryDto>(It.IsAny<Category>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(AppIGetMoneyInvalidCategoryException))]
        public async Task Delete_Category_Async_Test_BadPath_If_Category_Doesnt_Contain_User()
        {
            Category categoryTest = new Category()
            {
                Type = CategoryType.Expense,
                Name = "Travelling",
                PictureUrl = "thispictureUrl",
                Description = "This category is for the traveling expended money",
                IsDefault = false,
                Users = new List<User>(),
                Transactions = new List<Transaction>(),
            };

            _unitOfWorkMock.Setup(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId))
                .Returns(Task.FromResult((Category?)categoryTest));
            _userManagerMock.Setup(f => f.FindByIdAsync(FixtureDataCategory.UserId))
                .Returns(Task.FromResult(FixtureDataCategory.User));

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            await categoryAppService.DeleteCategoryAsync(FixtureDataCategory.CategoryId, FixtureDataCategory.UserId);

            _unitOfWorkMock.Verify(f => f.Categories.GetCategoryWithUsersByIdAsync(FixtureDataCategory.CategoryId), Times.Once);
            _userManagerMock.Verify(f => f.FindByIdAsync(FixtureDataCategory.UserId), Times.Once);
        }
    }
}