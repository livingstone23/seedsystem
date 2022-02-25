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
            return await _httpClient.GetFromJsonAsync<GroupPagination>($"api/groups/getbypagination/{(page)}/{pagesize}");
        }

        public async Task<Group> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Group>($"api/group/GetById/{id}");
            return result;
        }

        public async Task Save(Group group)
        {
            if (group.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Group>($"api/Groups/", group);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Group>($"api/Groups/{group.Id}", group);
            }
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Groups/{id}");
        }
    }
}
