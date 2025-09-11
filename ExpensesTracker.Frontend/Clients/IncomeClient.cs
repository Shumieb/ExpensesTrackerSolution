using Shared.Models;

namespace ExpensesTracker.Frontend.Clients
{
    public class IncomeClient(HttpClient httpClient)
    {
        public async Task<Income[]> GetAllIncomeAsync()
           => await httpClient.GetFromJsonAsync<Income[]>("income") ?? [];

        public async Task AddIncomeAsync(Income income)
            => await httpClient.PostAsJsonAsync("income", income);

        public async Task<Income> GetIncomeAsync(int id)
            => await httpClient.GetFromJsonAsync<Income>($"income/{id}")
                ?? throw new Exception("Could not find the income");

        public async Task UpdateIncomeAsync(Income income)
            => await httpClient.PutAsJsonAsync($"income/{income.IncomeId}", income);

        public async Task DeleteIncomeAsync(int id)
        => await httpClient.DeleteAsync($"income/{id}");
    }
}
