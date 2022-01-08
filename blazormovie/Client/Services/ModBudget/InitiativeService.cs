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


        public async Task<InitiativePagination> GetByPagination(int page, int pagesize)
        {
            return await _httpClient.GetFromJsonAsync<InitiativePagination>($"api/initiative/getbypagination/{(page)}/{pagesize}");
        }


        public async Task<Initiative> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Initiative>($"api/initiative/GetById/{id}");
            return result;
        }


        public async Task SaveInitiative(Initiative initiative)
        {
            if (initiative.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Initiative>($"api/Initiative/", initiative);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Initiative>($"api/Initiative/{initiative.Id}", initiative);
            }    
            
        }


        public async Task DeleteInitiative(int id)
        {
            await _httpClient.DeleteAsync($"api/Initiative/{id}");
        }


    }
}
