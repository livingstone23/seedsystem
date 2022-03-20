using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class ExchangeService : IExchangeService
    {
        private readonly HttpClient _httpClient;
        public ExchangeService(HttpClient httpClient)
        {
           
            _httpClient = httpClient;
    
        }
        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Exchanges>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Exchanges> GetByPoId(int poId)
        {
            var result = await _httpClient.GetFromJsonAsync<Exchanges>($"api/Exchange/GetByPoId/{poId}");
            return result;
        }

        public Task<bool> Insert(Exchanges exchange)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(Exchanges exchange)
        {
            throw new System.NotImplementedException();
        }
    }
}
