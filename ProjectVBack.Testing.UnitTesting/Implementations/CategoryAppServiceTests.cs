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
            _userManagerMock.Setup(f => f.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .Returns(Task.FromResult(new User()
                {
                    FirstName = "Guillermo",
                    LastName = "Cuenca",
                    Categories = new List<Category>(),
                    Transactions = new List<Transaction>()
                }));
            _userManagerMock.Setup(f => f.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User()
                {
                    FirstName = "Guillermo",
                    LastName = "Cuenca",
                    Categories = new List<Category>(),
                    Transactions = new List<Transaction>()
                }));

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(f => f.Categories.AddAsync(It.IsAny<Category>()))
                .Returns(Task.FromResult(new Category()
                {
                    Id = 1,
                    Name = "Esports",
                    PictureUrl = "websot.url",
                    Description = "Aqui me dejo los euros pa G2",
                    IsDefault = true,
                    Users = new List<User>(),
                    Transactions = new List<Transaction>()
                }));
            _unitOfWorkMock.Setup(f => f.Complete()).Verifiable();

            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(f => f.Map<Category>(It.IsAny<CategoryDto>()))
                .Returns(new Category()
                {
                    Id = 1,
                    Name = "Esports",
                    PictureUrl = "websot.url",
                    Description = "Aqui me dejo los euros pa G2",
                    IsDefault = true,
                    Users = new List<User>(),
                    Transactions = new List<Transaction>()
                });
            _mapperMock.Setup(f => f.Map<CategoryDto>(It.IsAny<Category>()))
                .Returns(new CategoryDto()
                {
                    Type = CategoryType.Income,
                    Id = 1,
                    Name = "Esports",
                    PictureUrl = "websot.url",
                    Description = "Aqui me dejo los euros pa G2",
                    IsDefault = true,
                });
            _mapperMock.Setup(f => f.Map<Category>(It.IsAny<AddCategoryRequest>()))
                .Returns(new Category()
                {
                    Type = CategoryType.Income,
                    Id = 1,
                    Name = "Esports",
                    PictureUrl = "websot.url",
                    Description = "Aqui me dejo los euros pa G2",
                    IsDefault = true,
                });


            var errorList = new List<FluentValidation.Results.ValidationFailure>();

            _editCategoryRequestValidatorMock = new Mock<IValidator<EditCategoryRequest>>();
            _editCategoryRequestValidatorMock.Setup(f => f.Validate(It.IsAny<EditCategoryRequest>()))
                .Returns(new FluentValidation.Results.ValidationResult(errorList));

            _addCategoryRequestValidatorMock = new Mock<IValidator<AddCategoryRequest>>();
            _addCategoryRequestValidatorMock.Setup(f => f.Validate(It.IsAny<AddCategoryRequest>()))
                .Returns(new FluentValidation.Results.ValidationResult(errorList));
        }

        [TestMethod()]
        public void CategoryAppServiceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateCategoryAsyncTest_GoodPath()
        {
            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = categoryAppService.CreateCategoryAsync(new AddCategoryRequest("Esports", "Antonio", "AntonioLopez", CategoryType.Expense), "vbnmkl").Result;

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result , typeof(CategoryDto));
        }

        [TestMethod()]
        [ExpectedException(typeof(AppIGetMoneyInvalidUserException))]
        public void CreateCategoryAsyncTest_BadPath()
        {
            

            var categoryAppService = new CategoryAppService(_unitOfWorkMock.Object, _userManagerMock.Object, _mapperMock.Object,
                _editCategoryRequestValidatorMock.Object, _addCategoryRequestValidatorMock.Object);

            var result = categoryAppService.CreateCategoryAsync(new AddCategoryRequest("Esports", "Antonio", "AntonioLopez", CategoryType.Expense), "vbnmkl").Result;
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
        public void EditCategoryAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteCategoryAsyncTest()
        {
            Assert.Fail();
        }
    }
}