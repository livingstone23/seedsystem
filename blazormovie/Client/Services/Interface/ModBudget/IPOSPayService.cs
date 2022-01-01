using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IPOSPayService
    {
        Task<IEnumerable<POSPay>> GetPOSPays();
        Task<POSPay> GetById(int id);
        Task<IEnumerable<POSPay>> GetByInitiative(int initiativeId);
        Task<IEnumerable<POSPay>> GetByProject(int projectId);
        Task SavePOSPay(POSPay pOSPay);





        //TODO
        Task DeletePOSPay(int id);

    }
}
