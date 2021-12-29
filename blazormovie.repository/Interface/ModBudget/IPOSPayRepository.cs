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
        Task<IEnumerable<POSPay>> GetAll();
        Task<bool> Insert(POSPay posPay);
        Task<POSPay> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> Update(POSPay posPay);

    }
}
