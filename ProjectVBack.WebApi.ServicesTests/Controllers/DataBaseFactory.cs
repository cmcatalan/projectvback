using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProjectVBack.Infrastructure.Persistence;

namespace ProjectVBack.Testing.IntegrationTesting.Controllers
{
    public static class DataBaseFactory
    {
        private readonly static string _connectionString = "Server = localhost; Database=IGetMoneyTest;Uid=root;Pwd=my-secret-pw;";

        public static MoneyAppContext CreateTestDataBase()
        {
            var options = new DbContextOptionsBuilder<MoneyAppContext>().UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString)).Options;
            var dbcontext = new MoneyAppContext(options);
            dbcontext.Database.Migrate();

            return dbcontext;
        }

        public static MoneyAppContext CreateInMemoryDataBase()
        {
            var options = new DbContextOptionsBuilder<MoneyAppContext>().
                UseInMemoryDatabase("IGetMoneyMemoryTest").
                ConfigureWarnings(f => f.Ignore(InMemoryEventId.TransactionIgnoredWarning)).
                Options;
            var dbcontext = new MoneyAppContext(options);

            return dbcontext;
        }

        public static void DisposeContext(MoneyAppContext databaseContext)
        {
            databaseContext.Database.EnsureDeleted();
            databaseContext.Dispose();
        }
    }
}
