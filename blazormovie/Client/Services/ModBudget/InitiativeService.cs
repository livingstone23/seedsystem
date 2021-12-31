using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class InitiativeService : IInitiativeService
    {
        private readonly HttpClient _httpClient;

        public InitiativeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Initiative>> GetInitiatives()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Initiative>>($"api/initiative");
        }
    }
}
