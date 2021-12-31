using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class POSPayService : IPOSPayService
    {
        private readonly HttpClient _httpClient;

        public POSPayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<POSPay>> GetPOSPays()
        {
            throw new System.NotImplementedException();
        }
    }
}
