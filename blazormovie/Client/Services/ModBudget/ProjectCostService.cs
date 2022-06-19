using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class ProjectCostService : IProjectCostService
    {
        private readonly HttpClient _httpClient;

        public ProjectCostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/project/cost/{id}");
        }

        public async Task Save(ProjectCost projectCosts)
        {
            await _httpClient.PostAsJsonAsync<ProjectCost>($"api/project/Costs", projectCosts);
        }

  
    }
}
