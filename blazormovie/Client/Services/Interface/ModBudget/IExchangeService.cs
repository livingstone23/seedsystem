using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IExchangeService
    {
        Task<IEnumerable<Exchanges>> GetAll();
        Task<bool> Insert(Exchanges exchange);
        Task<Exchanges> GetByPoId(int poId);
        Task<bool> Delete(int id);
        Task<bool> Update(Exchanges exchange);
    }
}
