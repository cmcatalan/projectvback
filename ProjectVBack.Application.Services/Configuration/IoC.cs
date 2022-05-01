using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Infrastructure.Persistence;
using ProjectVBack.Infrastructure.Repositories;

namespace ProjectVBack.Application.Services.Configuration
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString:DefaultConnection"];
            var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 23));

            //services.AddAutoMapper(typeof(AppProfile));

            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<ITransactionsRepository, TransactionsRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<MoneyAppContext>(opts => opts.UseMySql(connectionString, mySqlServerVersion))
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MoneyAppContext>();
            //services.AddTransient<IRepository<Invoice>, InvoiceRepository>();

            return services;
        }
    }
}
