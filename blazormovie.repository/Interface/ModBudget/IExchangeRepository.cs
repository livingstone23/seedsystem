using blazormovie.Shared.Entities;
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
    }
}
