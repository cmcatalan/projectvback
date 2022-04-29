using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectVBack.Domain.Entities;
using ProjectVBack.Crosscutting.Utils;

namespace ProjectVBack.Infrastructure.Persistence
{
    public class MoneyAppContext : IdentityDbContext<User>
    {
        const string CategoriesTableName = "Categories";
        const string TransactionsTableName = "Transactions";
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
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