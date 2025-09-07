using ExpensesTracker.Backend.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExpensesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpensesDb")));

var app = builder.Build();

// expenses routes
RouteGroupBuilder expenses = app.MapGroup("/expenses");
expenses.MapGet("/", GetAllExpenses);
expenses.MapGet("/{id}", GetExpense);
expenses.MapPost("/", CreateExpense);
expenses.MapPut("/{id}", UpdateExpense);
expenses.MapDelete("/{id}", DeleteExpense);

// income routes
RouteGroupBuilder income = app.MapGroup("/income");
income.MapGet("/", GetAllIncome);
income.MapGet("/{id}", GetIncome);
income.MapPost("/", CreateIncome);
income.MapPut("/{id}", UpdateIncome);
income.MapDelete("/{id}", DeleteIncome);

// category routes
RouteGroupBuilder categories = app.MapGroup("/categories");
categories.MapGet("/", GetAllCategories);
categories.MapGet("/{id}", GetCategory);

// frequency routes
RouteGroupBuilder frequencies = app.MapGroup("/frequencies");
frequencies.MapGet("/", GetAllFrequencies);
frequencies.MapGet("/{id}", GetFrequency);

app.Run();

// get all expenses
static async Task<IResult> GetAllExpenses(ExpensesContext db)
{
    return TypedResults.Ok(
        await db.Expenses
        .Include(x => x.Category)
        .Include(x => x.Frequency)
        .ToArrayAsync());
}

// get expense by id
static async Task<IResult> GetExpense(int id, ExpensesContext db)
{
    var expense = await db.Expenses
        .Include(x => x.Category)
        .Include(x => x.Frequency)
        .FirstOrDefaultAsync(x => x.ExpenseId == id);

    return expense is not null
        ? TypedResults.Ok(expense)
        : TypedResults.NotFound();
}

// create an expense
static async Task<IResult> CreateExpense(Expense expense, ExpensesContext db)
{
    await db.Expenses.AddAsync(expense);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/expenses/{expense.ExpenseId}", expense);
}

// update an expense
static async Task<IResult> UpdateExpense(int id, Expense expense, ExpensesContext db)
{

    var foundExpense = await db.Expenses.FindAsync(id);

    if (foundExpense is null) return TypedResults.NotFound();

    foundExpense.Name = expense.Name;
    foundExpense.Amount = expense.Amount;
    foundExpense.CategoryId = expense.CategoryId;
    foundExpense.IsPaid = expense.IsPaid;
    foundExpense.FrequencyId = expense.FrequencyId;
    foundExpense.DueDate = expense.DueDate;    

    await db.SaveChangesAsync();
    return TypedResults.Ok(foundExpense);
}

// delete an expense
static async Task<IResult> DeleteExpense(int id, ExpensesContext db)
{
    if (await db.Expenses.FindAsync(id) is Expense expense)
    {
        db.Expenses.Remove(expense);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NoContent();
}

// get all income
static async Task<IResult> GetAllIncome(ExpensesContext db)
{
    return TypedResults.Ok(
        await db.Income
        .ToArrayAsync());
}

// get income by id
static async Task<IResult> GetIncome(int id, ExpensesContext db)
{
    var income = await db.Income.FindAsync(id);

    return income is not null
        ? TypedResults.Ok(income)
        : TypedResults.NotFound();
}

// create an income
static async Task<IResult> CreateIncome(Income income, ExpensesContext db)
{
    await db.Income.AddAsync(income);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/income/{income.IncomeId}", income);
}

// update an income
static async Task<IResult> UpdateIncome(int id, Income income, ExpensesContext db)
{

    var foundIncome = await db.Income.FindAsync(id);

    if (foundIncome is null) return TypedResults.NotFound();

    foundIncome.Name = income.Name;
    foundIncome.Amount = income.Amount;

    await db.SaveChangesAsync();
    return TypedResults.Ok(foundIncome);
}

// delete an income
static async Task<IResult> DeleteIncome(int id, ExpensesContext db)
{
    if (await db.Income.FindAsync(id) is Income income)
    {
        db.Income.Remove(income);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NoContent();
}

// get all categories
static async Task<IResult> GetAllCategories(ExpensesContext db)
{
    return TypedResults.Ok(
        await db.Categories
        .ToArrayAsync());
}

// get category by id
static async Task<IResult> GetCategory(int id, ExpensesContext db)
{
    var category = await db.Categories.FindAsync(id);

    return category is not null
        ? TypedResults.Ok(category)
        : TypedResults.NotFound();
}

// get all frequencies
static async Task<IResult> GetAllFrequencies(ExpensesContext db)
{
    return TypedResults.Ok(
        await db.Frequencies
        .ToArrayAsync());
}

// get category by id
static async Task<IResult> GetFrequency(int id, ExpensesContext db)
{
    var frequency= await db.Frequencies.FindAsync(id);

    return frequency is not null
        ? TypedResults.Ok(frequency)
        : TypedResults.NotFound();
}

