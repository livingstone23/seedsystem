using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blazormovie.Shared.SeedEntities;
namespace blazormovie.repository.Interface.ModBudget
{
    public interface ICostRepository
    {
        Task<IEnumerable<Cost>> GetAll();

        Task<PagingResponseModel<List<Cost>>> GetCostsByPagination(int currentPageNumber, int pageSize);
        Task<Cost> GetById(int id);

        Task<bool> Insert(Cost cost);

        Task<bool> Delete(int id);

        Task<bool> Update(Cost cost);

        Task<IEnumerable<Project>> GetProjectsByCost(int costId);

        //Task<bool> InsertProjectCost(int projectId, Group group);

        //Task<bool> DeleteClientGroup(int clientId);

        //Task<IEnumerable<Group>> GetGroupsByClient(int clientId);
    }
}
