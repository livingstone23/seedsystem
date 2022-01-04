using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class POSPayService : IPOSPayService
    {
        private readonly HttpClient _httpClient;

        public  POSPayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<POSPay>> GetPOSPays()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<POSPay>>($"api/pospay");
        }

        public async Task<POSPayPagination> GetByPagination(int page, int pagesize)
        {
            //return await _httpClient.GetFromJsonAsync<POSPayPagination>($"api/pospay/getbypagination/?currentPageNumber={(page + 1)}&pagesize={pagesize}");
            return await _httpClient.GetFromJsonAsync<POSPayPagination>($"api/pospay/getbypagination/{(page)}/{pagesize}");
        }


        public async Task<POSPay> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<POSPay>($"api/pospay/GetById/{id}");
        }


        public async Task<IEnumerable<POSPay>> GetByInitiative(int initiativeId)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<POSPay>>($"api/pospay/GetByInitiative/{initiativeId}");
            return result;
        }


        public async Task<IEnumerable<POSPay>> GetByProject(int projectId)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<POSPay>>($"api/pospay/GetByProject/{projectId}");
            return result;
        }


        public async Task SavePOSPay(POSPay pOSPay)
        {
            if (pOSPay.Id == 0)
                await _httpClient.PostAsJsonAsync<POSPay>($"api/pospay/", pOSPay);

        }

        public Task DeletePOSPay(int id)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
