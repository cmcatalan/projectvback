using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectVBack.Infrastructure.Persistence
{
    public class MoneyAppContext : IdentityDbContext<User>
    {
        public MoneyAppContext(DbContextOptions<MoneyAppContext> options) : base(options)
        {
        }
    }
}