using ProjectVBack.Application.Dtos;
using ProjectVBack.Application.Dtos.CategoryService;
using ProjectVBack.Crosscutting.Utils;
using ProjectVBack.Domain.Entities;
using System.Collections.Generic;

public static class FixtureDataCategory
{
    public static string UserId = "1";
    public static int CategoryId = 1;

    public static Category Category = new Category
    {
        Type = CategoryType.Expense,
        Name = "Travelling",
        PictureUrl = "thispictureUrl",
        Description = "This category is for the traveling expended money",
        IsDefault = false,
        Users = new List<User>(),
        Transactions = new List<Transaction>(),
    };

    public static CategoryDto CategoryDto = new CategoryDto
    {
        Type = CategoryType.Expense,
        Name = "Travelling",
        PictureUrl = "thispictureUrl",
        Description = "This category is for the traveling expended money",
        IsDefault = false,
        Id = 5
    };

    public static User User = new User
    {
        FirstName = "Guillermo",
        LastName = "Cuenca",
        Categories = new List<Category>(),
        Transactions = new List<Transaction>()
    };

    public static AddCategoryRequest AddCategoryRequest = new AddCategoryRequest("Travelling", "thispictureUrl",
        "This category is for the traveling expended money", CategoryType.Expense);

    public static EditCategoryRequest EditCategoryRequest = new EditCategoryRequest("Travelling", "thispictureUrl",
        "This category is for the traveling expended money", CategoryType.Expense , 1);

    public static IEnumerable<FluentValidation.Results.ValidationFailure> ErrorList = new List<FluentValidation.Results.ValidationFailure>();
    public static IEnumerable<Category> CategoryList = new List<Category>() { Category };
    public static IEnumerable<CategoryDto> CategoryDtoList = new List<CategoryDto>() { CategoryDto };

    public static FluentValidation.Results.ValidationResult ValidationResult = new FluentValidation.Results.ValidationResult();
}
