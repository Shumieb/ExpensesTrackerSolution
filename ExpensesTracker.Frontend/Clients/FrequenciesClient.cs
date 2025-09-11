using Shared.Models;

namespace ExpensesTracker.Frontend.Clients
{
    public class FrequenciesClient(HttpClient httpClient)
    {
        public async Task<Frequency[]> GetFrequenciesAsync()
          => await httpClient.GetFromJsonAsync<Frequency[]>("frequencies") ?? [];

        public async Task<Frequency> GetFrequencyAsync(int id)
            => await httpClient.GetFromJsonAsync<Frequency>($"frequencies/{id}")
                ?? throw new Exception("Could not find the frequency");
    }
}
