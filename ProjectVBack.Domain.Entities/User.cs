using Microsoft.AspNetCore.Identity;

namespace ProjectVBack.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Category> Categories { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}