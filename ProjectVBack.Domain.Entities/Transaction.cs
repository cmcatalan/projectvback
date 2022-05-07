using System.ComponentModel.DataAnnotations;

namespace ProjectVBack.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; set; } = string.Empty;

        [Required]
        public double Value { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public User? User { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
