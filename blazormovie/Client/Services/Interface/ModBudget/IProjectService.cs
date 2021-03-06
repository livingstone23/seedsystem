using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjects();
        
        Task<ProjectPagination> GetByPagination(int currentPage, int pagesize);

        Task<ProjectPagination> GetByPaginationAndInitiativeId(int currentPage, int pagesize, int initiativeId);

        Task<Project> GetById(int id);

        Task<IEnumerable<Project>> GetByInitiative(int initiativeId);

        Task SaveProject(Project project);
                       
        Task DeleteProject(int id);
    }
}
