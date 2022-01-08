using blazormovie.Shared.SeedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<PagingResponseModel<List<Project>>> GetProjectByPagination(int currentPageNumber, int pageSize);
        Task<Project> GetById(int id);
        Task<IEnumerable<Project>> GetByInitiative(int initiativeid);
        Task<bool> SaveProject(Project project);
        
        Task<bool> Delete(int id);
        Task<bool> Update(Project project);

        

    }
}
