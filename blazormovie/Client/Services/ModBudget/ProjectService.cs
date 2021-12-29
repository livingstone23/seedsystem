using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class ProjectService : IProjectService
    {
        public Task<IEnumerable<Project>> GetProjects()
        {
            throw new System.NotImplementedException();
        }
    }
}
