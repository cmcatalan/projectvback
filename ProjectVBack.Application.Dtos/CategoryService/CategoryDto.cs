using ProjectVBack.Crosscutting.Utils;
using ProjectVBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectVBack.Application.Dtos.CategoryService
{
    public class CategoryDto
    {
        public CategoryType Type { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
