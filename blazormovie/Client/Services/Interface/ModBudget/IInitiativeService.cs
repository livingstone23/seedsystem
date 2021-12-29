using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IInitiativeService
    {
        Task<IEnumerable<Budget>> GetInitiatives();

    }
}
