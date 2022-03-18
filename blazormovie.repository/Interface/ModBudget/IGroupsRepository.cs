using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blazormovie.Shared.SeedEntities;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IGroupsRepository
    {

        Task<IEnumerable<Group>> GetAll();

        Task<PagingResponseModel<List<Group>>> GetGroupsByPagination(int currentPageNumber, int pageSize);

        Task<PagingResponseModel<List<Group>>> GetGroupsByPaginationAndClientId(int currentPageNumber, int pageSize, int clientId);

        Task<List<Group>> GetByClientId(int id);

        Task<Group> GetById(int id);

        Task<bool> Insert(Group group);

        Task<bool> Delete(int id);
        Task<IEnumerable<Initiative>> GetInitiativeByGroup(int id);
        Task<bool> Update(Group group);
        Task<bool> InsertInitiative(int groupId, Initiative initiative);
        Task<bool> DeleteInitiativeGroup(int InitiativeId);
        Task<IEnumerable<InitiativeGroup>>GetInitiativesGroups(int initiativeId);
       

    }
}
