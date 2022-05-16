using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blazormovie.Shared.SeedEntities;

namespace blazormovie.repository.Interface.ModBudget
{
    public  interface IClientRepository
    {

        Task<IEnumerable<Client>> GetAll();

        Task<PagingResponseModel<List<Client>>> GetClientsByPagination(int currentPageNumber, int pageSize);
        Task<Client> GetById(int id);

        Task<bool> Insert(Client client);

        Task<bool> Delete(int id);

        Task<bool> Update(Client client);

        Task<bool> InsertClientGroup(int clientId, Group group);

        Task<bool> DeleteClientGroup(int clientId);

        Task<IEnumerable<Group>> GetGroupsByClient(int clientId);
        Task<IEnumerable<ClientGroup>> GetClientGroup();
    }
}
