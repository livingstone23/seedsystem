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
        
        
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Project>>($"api/project");
        }


        public async Task<ProjectPagination> GetByPagination(int page, int pagesize)
        {
            return await _httpClient.GetFromJsonAsync<ProjectPagination>($"api/project/getbypagination/{(page)}/{pagesize}");
        }


        public async Task<Project> GetById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Project>($"api/project/GetById/{id}");
        }


        public async Task<IEnumerable<Project>> GetByInitiative(int initiativeId)
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<Project>>($"api/project/GetByInitiative/{initiativeId}");
            return result;
        }


        public async Task SaveProject(Project project)
        {
            if (project.Id == 0)
                await _httpClient.PostAsJsonAsync<Project>($"api/project/", project);

        }


        public Task DeleteProject(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
