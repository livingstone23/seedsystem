using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;

namespace blazormovie.Client.Services.ModBudget
{
    public class ClientService: IClientService
    {

        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<blazormovie.Shared.SeedEntities.Client>> Get()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<blazormovie.Shared.SeedEntities.Client>>($"api/Clients");
        }

        public async Task<ClientPagination> GetByPagination(int page, int pagesize)
        {
            return await _httpClient.GetFromJsonAsync<ClientPagination>($"api/Clients/getbypagination/{(page)}/{pagesize}");
        }

        public async Task<blazormovie.Shared.SeedEntities.Client> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<blazormovie.Shared.SeedEntities.Client>($"api/Clients/GetById/{id}");
            return result;
        }

        public async Task Save(blazormovie.Shared.SeedEntities.Client client)
        {
            if (client.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<blazormovie.Shared.SeedEntities.Client>($"api/Clients/", client);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<blazormovie.Shared.SeedEntities.Client>($"api/Clients/{client.Id}", client);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Clients/{id}");
        }
    }
}
