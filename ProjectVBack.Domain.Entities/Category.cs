using ProjectVBack.Crosscutting.Utils;
using System.ComponentModel.DataAnnotations;

namespace ProjectVBack.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public CategoryType Type { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; } = false;

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
