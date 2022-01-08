using blazormovie.Shared.SeedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IPOSPayRepository
    {
        Task<IEnumerable<POSPay>> GetPOSPays();
        Task <PagingResponseModel<List<POSPay>>> GetPOSPaysByPagination(int currentPageNumber, int pageSize);
        Task<PagingResponseModel<List<POSPayDTO>>> GetPOSPayDtosByPagination(int currentPageNumber, int pageSize);
        Task<POSPay> GetById(int id);
        Task<IEnumerable<POSPay>> GetByInitiative(int initiativeId);
        Task<IEnumerable<POSPay>> GetByProject(int projectId);
        Task<bool> SavePOSPay(POSPay posPay);
        
        Task<bool> Delete(int id);
        Task<bool> Update(POSPay posPay);

    }
}
