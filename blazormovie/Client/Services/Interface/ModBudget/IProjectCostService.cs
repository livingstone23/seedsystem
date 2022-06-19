using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IProjectCostService
    {

        Task Save(ProjectCost projectCosts);

        Task Delete(int id);
    }
}
