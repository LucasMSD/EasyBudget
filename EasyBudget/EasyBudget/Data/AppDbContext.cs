using EasyBudget.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyBudget.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movement>()
                .HasOne(movement => movement.Category)
                .WithMany(category => category.Movements)
                .HasForeignKey(movement => movement.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Transport", Type = Enums.FinancialType.Expense, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 2, Name = "Food", Type = Enums.FinancialType.Expense, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 3, Name = "Groceries", Type = Enums.FinancialType.Expense, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 4, Name = "Health", Type = Enums.FinancialType.Expense, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 5, Name = "Work", Type = Enums.FinancialType.Expense, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 6, Name = "Home", Type = Enums.FinancialType.Expense, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 7, Name = "Investments", Type = Enums.FinancialType.Expense, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 8, Name = "Others expenses", Type = Enums.FinancialType.Expense, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 9, Name = "Salary", Type = Enums.FinancialType.Income, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 10, Name = "Investments", Type = Enums.FinancialType.Income, Created = DateTime.Now, Updated = DateTime.Now },
                new Category { Id = 11, Name = "Other incomes", Type = Enums.FinancialType.Income, Created = DateTime.Now, Updated = DateTime.Now });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movement> Movements { get; set; }
    }
}
