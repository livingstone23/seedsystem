using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blazormovie.Shared.SeedEntities;

namespace blazormovie.repository.Interface.ModBudget
{
    public  interface IClientGroupRepository
    {

        Task<bool> InsertGroup(int clientId, Group group);
        Task<IEnumerable<Group>> GetByGroup(int clientId);
        Task<bool> DeleteGroupsByClient(int clientId);

    }
}
