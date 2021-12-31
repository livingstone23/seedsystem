using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjects();

        Task<IEnumerable<Project>> GetByInitiative(int initiativeId);

    }
}
