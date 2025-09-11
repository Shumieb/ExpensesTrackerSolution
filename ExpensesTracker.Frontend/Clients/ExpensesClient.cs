using Shared.Models;

namespace ExpensesTracker.Frontend.Clients
{
    public class ExpensesClient(HttpClient httpClient)
    {
        public async Task<Expense[]> GetExpensesAsync()
           => await httpClient.GetFromJsonAsync<Expense[]>("expenses") ?? [];

        public async Task AddExpenseAsync(Expense expense)
            => await httpClient.PostAsJsonAsync("expenses", expense);

        public async Task<Expense> GetExpenseAsync(int id)
            => await httpClient.GetFromJsonAsync<Expense>($"expenses/{id}")
                ?? throw new Exception("Could not find the expense");

        public async Task UpdateExpenseAsync(Expense expense)
            => await httpClient.PutAsJsonAsync($"expenses/{expense.ExpenseId}", expense);

        public async Task DeleteExpenseAsync(int id)
        => await httpClient.DeleteAsync($"expenses/{id}");

    }
}
