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


        public async Task<Initiative> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Initiative>($"api/initiative/GetById/{id}");
            return result;
        }


        public async Task SaveInitiative(Initiative initiative)
        {
            if (initiative.Id == 0)
                await _httpClient.PostAsJsonAsync<Initiative>($"api/Initiative/", initiative);
            
        }


        public async Task DeleteInitiative(int id)
        {
            throw new System.NotImplementedException();
        }


    }
}
