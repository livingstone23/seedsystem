using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface ICostService
    {
        Task<IEnumerable<Cost>> Get();
        Task<Cost>GetById(int id);
    }
}
