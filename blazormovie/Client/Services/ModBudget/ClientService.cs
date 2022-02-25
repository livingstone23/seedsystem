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
            return await _httpClient.GetFromJsonAsync<IEnumerable<blazormovie.Shared.SeedEntities.Client>>($"api/Client");
        }

        public async Task<ClientPagination> GetByPagination(int page, int pagesize)
        {
            return await _httpClient.GetFromJsonAsync<ClientPagination>($"api/Client/getbypagination/{(page)}/{pagesize}");
        }

        public async Task<blazormovie.Shared.SeedEntities.Client> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<blazormovie.Shared.SeedEntities.Client>($"api/Client/GetById/{id}");
            return result;
        }

        public async Task Save(blazormovie.Shared.SeedEntities.Client client)
        {
            if (client.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<blazormovie.Shared.SeedEntities.Client>($"api/Client/", client);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<blazormovie.Shared.SeedEntities.Client>($"api/Client/{client.Id}", client);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Client/{id}");
        }
    }
}
