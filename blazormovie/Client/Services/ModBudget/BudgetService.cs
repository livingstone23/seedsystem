using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class BudgetService : IBudgetService
    {
        public Task<IEnumerable<Budget>> GetBudgets()
        {
            throw new System.NotImplementedException();
        }
    }
}
