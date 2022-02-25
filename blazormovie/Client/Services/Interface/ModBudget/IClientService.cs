using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IClientService
    {

        Task<IEnumerable<blazormovie.Shared.SeedEntities.Client>> Get();

        Task<ClientPagination> GetByPagination(int page, int pagesize);

        Task<blazormovie.Shared.SeedEntities.Client> GetById(int id);

        Task Save(blazormovie.Shared.SeedEntities.Client client);

        Task Delete(int id);

    }
}
