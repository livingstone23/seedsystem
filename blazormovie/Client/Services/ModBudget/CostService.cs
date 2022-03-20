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

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Cost/{id}");
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

        public async Task<CostPagination> GetByPagination(int page, int pagesize)
        {
            return await _httpClient.GetFromJsonAsync<CostPagination>($"api/Cost/GetByPagination/{(page)}/{pagesize}");
        }

        public async Task Save(Cost cost)
        {
            if (cost.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Cost>($"api/Cost/", cost);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Cost>($"api/Cost/{cost.Id}", cost);
            }
        }
    }
}
