using blazormovie.Shared.SeedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IInitiativeGroupRepository
    {
        Task<bool> InsertGroup(int clientId, Group group);
        Task<IEnumerable<Group>> GetByGroup(int clientId);
        Task<bool> DeleteGroupsByInitiative(int clientId);
    }
}
