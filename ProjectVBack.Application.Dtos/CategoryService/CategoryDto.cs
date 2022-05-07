using ProjectVBack.Crosscutting.Utils;

namespace ProjectVBack.Application.Dtos.CategoryService
{
    public class CategoryDto
    {
        public CategoryType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public int Id { get; set; }
    }
}
