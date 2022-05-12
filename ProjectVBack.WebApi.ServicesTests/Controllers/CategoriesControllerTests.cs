
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectVBack.Infrastructure.Persistence;
using ProjectVBack.Testing.IntegrationTesting.Controllers;
using System.Security.Claims;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectVBack.Controllers.Tests
{
    [TestClass()]
    public class CategoriesControllerTests
    {
        protected MoneyAppContext TestDbContext { get; private set; }

        [TestInitialize]
        public void TestSetup()
        {
            TestDbContext = DataBaseFactory.CreateTestDataBase();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            DataBaseFactory.DisposeContext(TestDbContext);
        }

        [TestMethod()]
        public void CategoriesControllerTest()
        {
           Assert.Fail();
        }

        [TestMethod()]
        public void AddCategoryTest()
        {
            var controller = new CategoriesController();
        }

        [TestMethod()]
        public void GetCategoryByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllCategoriesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditCategoryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteCategoryTest()
        {
            Assert.Fail();
        }
    }
}