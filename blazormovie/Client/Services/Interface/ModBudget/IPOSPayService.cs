using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IPOSPayService
    {
        Task<IEnumerable<POSPay>> GetPOSPays();
        
        Task<POSPayPagination> GetByPagination(int currentPage, int pagesize);
        
        Task<POSPayDTOPagination> GetByPaginationDto(int currentPage, int pagesize);
        
        Task<POSPay> GetById(int id);
        
        Task<IEnumerable<POSPay>> GetByInitiative(int initiativeId);
        
        Task<IEnumerable<POSPay>> GetByProject(int projectId);
        
        Task SavePOSPay(POSPay pOSPay);

        Task DeletePOSPay(int id);

    }
}
