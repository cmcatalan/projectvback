using Microsoft.AspNetCore.Identity;

namespace ProjectVBack.Infrastructure.DataModels
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Category> Users { get; set; }
    }
}