using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class CostService : ICostService
    {
        private readonly HttpClient _httpClient;
        public CostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Cost>> Get()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Cost>>($"api/Cost");
        }

        public async Task<Cost> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Cost>($"api/Cost/GetById/{id}");
            return result;
        }
    }
}
