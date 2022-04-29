using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Infrastructure.Persistence;

namespace ProjectVBack.Application.Services.Configuration
{
    public static class SeedAppBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedData(this IApplicationBuilder app)
        {
            var scopeFactory = app!.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<MoneyAppContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            context.Database.EnsureCreated();

            if (!userManager.Users.Any())
            {
                var adminRole = "Admin";
                var userRole = "User";

                await roleManager.CreateAsync(new IdentityRole { Name = adminRole });
                await roleManager.CreateAsync(new IdentityRole { Name = userRole });

                var defaultPassword = "P@ss.W0rd";

                var newUser = new User
                {
                    Email = "guillermo.cuenca@col.vueling.com",
                    FirstName = "Guillermo",
                    LastName = "Cuenca",
                    UserName = "guillermo.cuenca"
                };

                await userManager.CreateAsync(newUser, defaultPassword);

                await userManager.AddToRoleAsync(newUser, adminRole);
                await userManager.AddToRoleAsync(newUser, userRole);

                var newUser1 = new User
                {
                    Email = "cesar.catalan@col.vueling.com",
                    FirstName = "Miguel",
                    LastName = "Catalan",
                    UserName = "miguel.catalan"
                };
                await userManager.CreateAsync(newUser1, defaultPassword);

                await userManager.AddToRoleAsync(newUser1, adminRole);
                await userManager.AddToRoleAsync(newUser1, userRole);
            }

            return app;
        }
    }
}
