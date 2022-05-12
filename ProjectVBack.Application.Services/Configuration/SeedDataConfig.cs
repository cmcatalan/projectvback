using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProjectVBack.Crosscutting.Utils;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;
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

            context.Database.EnsureCreated();

            await CategoriesSeed(scope);
            await UsersSeed(scope);

            return app;
        }

        private async static Task UsersSeed(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var defaultCategories = await unitOfWork.Categories.GetDefaultCategoriesAsync();

            var haveUsers = userManager.Users.Any();

            if (!haveUsers)
            {
                var defaultRoles = new List<string>() {
                    RoleUtils.Admin,
                    RoleUtils.User
                };

                foreach (var role in defaultRoles)
                    await roleManager.CreateAsync(new IdentityRole(role));

                var defaultPassword = "P@ss.W0rd";

                var usersToAdd = new List<User>() {
                    new User
                    {
                        Email = "guillermo.cuenca@col.vueling.com",
                        FirstName = "Guillermo",
                        LastName = "Cuenca",
                        UserName = "guillermo.cuenca",
                        Categories = defaultCategories.ToList()

                    },
                    new User
                    {
                        Email = "cesar.catalan@col.vueling.com",
                        FirstName = "Miguel",
                        LastName = "Catalan",
                        UserName = "miguel.catalan",
                        Categories = defaultCategories.ToList()
                    }
                };

                unitOfWork.Complete();

                foreach (var user in usersToAdd)
                {
                    await userManager.CreateAsync(user, defaultPassword);

                    foreach (var defaultRole in defaultRoles)
                        await userManager.AddToRoleAsync(user, defaultRole);
                }
            }
        }

        private async static Task CategoriesSeed(IServiceScope scope)
        {
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var haveCategories = unitOfWork.Categories.Query().Any();

            if (!haveCategories)
            {
                var defaultCategories = new List<Category>() {
                    new Category(){
                        Id = 1,
                        Type= CategoryType.Expense,
                        Name = "Bills",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/1611/1611154.png",
                        Description = "",
                        IsDefault = true,
                    },
                    new Category(){
                        Id= 2,
                        Type= CategoryType.Expense,
                        Name = "Car",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/846/846338.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id =3,
                        Type= CategoryType.Expense,
                        Name = "Clothes",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/863/863684.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id =4,
                        Type= CategoryType.Expense,
                        Name = "Communications",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/545/545245.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id =5,
                        Type= CategoryType.Expense,
                        Name = "Eating out",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/599/599995.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id =6,
                        Type= CategoryType.Expense,
                        Name = "Entertainment",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/4221/4221836.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 7,
                        Type= CategoryType.Expense,
                        Name = "Food",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/1919/1919608.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 8,
                        Type= CategoryType.Expense,
                        Name = "Gifts",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/548/548427.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 9,
                        Type= CategoryType.Expense,
                        Name = "Health",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/898/898655.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 10,
                        Type= CategoryType.Expense,
                        Name = "House",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/709/709873.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 11,
                        Type= CategoryType.Expense,
                        Name = "Pets",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/2527/2527324.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 12,
                        Type= CategoryType.Expense,
                        Name = "Sports",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/6314/6314992.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 13,
                        Type= CategoryType.Expense,
                        Name = "Taxi",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/5900/5900567.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 14,
                        Type= CategoryType.Expense,
                        Name = "Toiletry",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/100/100520.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 15,
                        Type= CategoryType.Expense,
                        Name = "Transport",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/1034/1034897.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 16,
                        Type= CategoryType.Income,
                        Name = "Deposits",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/5525/5525188.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 17,
                        Type= CategoryType.Income,
                        Name = "Salary",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/1077/1077976.png",
                        Description = "",
                        IsDefault = true,
                    },new Category(){
                        Id = 18,
                        Type= CategoryType.Income,
                        Name = "Savings",
                        PictureUrl = "https://cdn-icons-png.flaticon.com/512/2746/2746077.png",
                        Description = "",
                        IsDefault = true,
                    },
                };

                await unitOfWork.Categories.AddRangeAsync(defaultCategories);

                unitOfWork.Complete();
            }
        }

    }
}
