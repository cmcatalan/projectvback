using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectVBack.Infrastructure.Persistence;

namespace ProjectVBack.Application.Services.Configuration
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString:DefaultConnection"];
            var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 28));

            //services.AddAutoMapper(typeof(AppProfile));
            services.AddDbContext<MoneyAppContext>(opts => opts.UseMySql(connectionString, mySqlServerVersion))
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MoneyAppContext>();
            //services.AddTransient<IRepository<Invoice>, InvoiceRepository>();

            return services;
        }
    }
}
