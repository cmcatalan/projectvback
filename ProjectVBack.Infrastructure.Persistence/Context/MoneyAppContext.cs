using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectVBack.Crosscutting.Utils;
using ProjectVBack.Domain.Entities;

namespace ProjectVBack.Infrastructure.Persistence
{
    public class MoneyAppContext : IdentityDbContext<User>
    {
        const string CategoriesTableName = "Categories";
        const string TransactionsTableName = "Transactions";
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public MoneyAppContext(DbContextOptions<MoneyAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Category>()
                .Property(e => e.Type)
                .HasConversion(
                    toDatabase => toDatabase.ToString(),
                    fromDatabase => (CategoryType)Enum.Parse(typeof(CategoryType), fromDatabase));

            modelBuilder.Entity<Category>().ToTable(CategoriesTableName);
            modelBuilder.Entity<Transaction>().ToTable(TransactionsTableName);
            modelBuilder.Entity<User>().ToTable("AspNetUsers");
        }
    }
}