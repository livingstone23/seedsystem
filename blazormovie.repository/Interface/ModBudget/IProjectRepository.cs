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
        Task<IEnumerable<Project>> GetAll();
        Task<bool> Insert(Project project);
        Task<Project> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Update(Project project);

    }
}
