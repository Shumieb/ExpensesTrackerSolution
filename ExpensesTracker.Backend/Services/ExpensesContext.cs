using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Shared.Models;

namespace ExpensesTracker.Backend.Services
{
    public class ExpensesContext : DbContext
    {
        public ExpensesContext(DbContextOptions<ExpensesContext> options)
           : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
       public DbSet<Category> Categories { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
          modelBuilder.Entity<Expense>().HasData(
                new Expense { ExpenseId = 1, Name = "Phone Bill", Amount = 20.99M, CategoryId = 1, FrequencyId=1, DueDate=12 },
                new Expense { ExpenseId = 2, Name = "Water Bill", Amount = 20.99M, CategoryId = 1, FrequencyId = 1, DueDate = 10 },
                new Expense { ExpenseId = 3, Name = "Rent", Amount = 200.99M, CategoryId = 2, FrequencyId = 1, DueDate = 8 },
                new Expense { ExpenseId = 4, Name = "Asda Grocery Shopping", Amount = 20.99M, CategoryId = 3, FrequencyId = 2, DueDate = 11 }
                );

            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId= 1, Name="Bills", Description="This category is for bills."},
               new Category { CategoryId = 2, Name = "Housing Costs", Description = "This category is for housing costs." },
               new Category { CategoryId = 3, Name = "Shopping", Description = "This category is for shopping costs." }
               );

            modelBuilder.Entity<Income>().HasData(
                new Income { IncomeId = 1, Name="Salary", Amount=1000}
                );

            modelBuilder.Entity<Frequency>().HasData(
                new Frequency { FrequencyId=1, Name="Monthly"},
                new Frequency { FrequencyId=2, Name="Once Only"}
                );
        }
    }
}
