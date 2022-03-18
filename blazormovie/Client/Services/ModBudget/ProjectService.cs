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

        public async Task<ProjectPagination> GetByPaginationAndInitiativeId(int page, int pagesize, int initiativeId)
        {
            return await _httpClient.GetFromJsonAsync<ProjectPagination>($"api/project/getbypaginationAndInitiativeId/{(page)}/{pagesize}/{initiativeId}");
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
            {
                var respuesta = await _httpClient.PostAsJsonAsync<Project>($"api/project/", project);
            } 
                else
            {
                var respuesta = await _httpClient.PutAsJsonAsync<Project>($"api/project/{project.Id}", project);
            }
        }


        public async Task DeleteProject(int id)
        {
            await _httpClient.DeleteAsync($"api/project/{id}");
        }

    }
}
