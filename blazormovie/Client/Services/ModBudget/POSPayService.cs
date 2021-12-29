using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class POSPayService : IPOSPayService
    {
        public Task<IEnumerable<POSPay>> GetPOSPays()
        {
            throw new System.NotImplementedException();
        }
    }
}
