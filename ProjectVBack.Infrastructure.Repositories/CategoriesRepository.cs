using ProjectVBack.Domain.Entities;
using ProjectVBack.Domain.Repositories.Abstractions;
using ProjectVBack.Infrastructure.Persistence;

namespace ProjectVBack.Infrastructure.Repositories
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(MoneyAppContext context) : base(context)
        {

        }
    }
}
