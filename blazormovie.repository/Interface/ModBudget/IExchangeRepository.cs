using blazormovie.Shared.Entities;
using blazormovie.Shared.SeedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IExchangeRepository
    {
        Task<IEnumerable<Exchanges>> GetAll();
        Task<bool> Insert(Exchanges exchange);
        Task<IEnumerable<Exchanges>> GetByPoId(int poId);
        Task<bool> Delete(int id);
        Task<bool> Update(Exchanges exchange);
    }
}
