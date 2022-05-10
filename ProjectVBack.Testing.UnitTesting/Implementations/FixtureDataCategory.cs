using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Dtos.CategoryService;
using ProjectVBack.Crosscutting.Utils;
using ProjectVBack.Domain.Entities;
using System.Collections.Generic;

public static class FixtureDataCategory
{
    public static string userId = "1";
    public static int categoryId = 1;

    public static Category category = new Category
    {
        Type = CategoryType.Expense,
        Name = "Travelling",
        PictureUrl = "thispictureUrl",
        Description = "This category is for the traveling expended money",
        IsDefault = false,
        Users = new List<User>(),
        Transactions = new List<Transaction>(),
    };

    public static CategoryDto categoryDto = new CategoryDto
    {
        Type = CategoryType.Expense,
        Name = "Travelling",
        PictureUrl = "thispictureUrl",
        Description = "This category is for the traveling expended money",
        IsDefault = false,
        Id = 5
    };

    public static User user = new User
    {
        FirstName = "Guillermo",
        LastName = "Cuenca",
        Categories = new List<Category>(),
        Transactions = new List<Transaction>()
    };

    public static AddCategoryRequest addCategoryRequest = new AddCategoryRequest("Travelling", "thispictureUrl",
        "This category is for the traveling expended money", CategoryType.Expense);

    public static EditCategoryRequest editCategoryRequest = new EditCategoryRequest("Travelling", "thispictureUrl",
    "This category is for the traveling expended money", CategoryType.Expense , 1);

    public static IEnumerable<FluentValidation.Results.ValidationFailure> errorList = new List<FluentValidation.Results.ValidationFailure>();

    public static FluentValidation.Results.ValidationResult validationResult = new FluentValidation.Results.ValidationResult();
}
