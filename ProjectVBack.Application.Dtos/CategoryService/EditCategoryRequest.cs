using ProjectVBack.Crosscutting.Utils;

namespace ProjectVBack.Application.Dtos
{
    public record EditCategoryRequest(string Name, string PictureUrl, string Description, CategoryType Type , int Id);
}