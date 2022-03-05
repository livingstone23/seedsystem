using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blazormovie.Shared.SeedEntities;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IGroupsInitiativeRepository
    {
        
        Task<bool> InsertGroup(int initiativeId, Group group);
        Task<IEnumerable<Group>> GetByGroup(int initiativeId);
        Task<bool> DeleteGroupsByInitiative(int initiativeId);

    }
}
