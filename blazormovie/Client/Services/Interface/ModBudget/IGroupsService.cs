using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using blazormovie.Shared.SeedEntities;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IGroupsService
    {
        Task<IEnumerable<Group>> Get();

        Task<GroupPagination> GetByPagination(int page, int pagesize);

        Task<GroupPagination> GetByPaginationAndClientId(int page, int pagesize, int ClientId);

        Task<Group> GetById(int id);

        Task Save(Group group);

        Task Delete(int id);
        Task DeleteInitiativeGroup(int initiativeId);
        Task InsertInitiativeGroup(int initiativeId,int groupId);
        Task GetInitiativeGroupById(int id);
    }
}
