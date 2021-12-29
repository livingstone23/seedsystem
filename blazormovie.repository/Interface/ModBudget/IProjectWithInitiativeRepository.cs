using blazormovie.Shared.SeedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IProjectWithInitiativeRepository
    {
        Task<IEnumerable<ProjectWithInitiative>> GetAll();
    }
}
