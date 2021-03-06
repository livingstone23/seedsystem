using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;

namespace blazormovie.Client.Services.ModBudget
{
    public class GroupsService: IGroupsService
    {

        private readonly HttpClient _httpClient;

        public GroupsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IEnumerable<Group>> Get()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Group>>($"api/groups");
        }

        public async Task<GroupPagination> GetByPagination(int page, int pagesize)
        {
            return await _httpClient.GetFromJsonAsync<GroupPagination>($"api/groups/getbypagination/{page}/{pagesize}");
        }

        public async Task<GroupPagination> GetByPaginationAndClientId(int page, int pagesize, int clientId)
        {
            return await _httpClient.GetFromJsonAsync<GroupPagination>($"api/groups/getbypaginationAndClientId/{page}/{pagesize}/{clientId}");
        }

        public async Task<Group> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Group>($"api/groups/GetById/{id}");
            return result;
        }


        public async Task Save(Group group)
        {
           
            if (group.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Group>($"api/groups/", group);
            }
            else
            {
                 await _httpClient.PutAsJsonAsync<Group>($"api/groups/{group.Id}", group);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/groups/{id}");
        }

        public async Task DeleteInitiativeGroup(int initiativeId)
        {
            await _httpClient.DeleteAsync($"api/groups/DeleteInitiativeGroupById/{initiativeId}");
        }

        public async Task InsertInitiativeGroup(int initiativeId, int groupId)
        {
            await _httpClient.PostAsJsonAsync<InitiativeGroup>($"api/groups/InsertInitiative/{initiativeId}/{groupId}", null);
        }

        public async Task GetInitiativeGroupById(int id)
        {
            await _httpClient.GetAsync($"api/GetInitiativeGroupsById/{id}");
        }
    }
}
