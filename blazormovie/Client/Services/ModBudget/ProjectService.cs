using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;

        public ProjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Project>> GetByInitiative(int initiativeId)
        {
            var result =  await _httpClient.GetFromJsonAsync<IEnumerable<Project>>($"api/project/GetByInitiative/{initiativeId}");
            return result;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Project>>($"api/project");
        }


    }
}
