using Shared.Models;

namespace ExpensesTracker.Frontend.Clients
{
    public class CategoriesClient(HttpClient httpClient)
    {
        public async Task<Category[]> GetCategoriesAsync()
           => await httpClient.GetFromJsonAsync<Category[]>("categories") ?? [];

        public async Task<Category> GetCategoryAsync(int id)
            => await httpClient.GetFromJsonAsync<Category>($"categories/{id}")
                ?? throw new Exception("Could not find the category");
    }
}
