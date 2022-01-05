using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IInitiativeService
    {
        Task<IEnumerable<Initiative>> GetInitiatives();
        Task<InitiativePagination> GetByPagination(int currentPage, int pagesize);
        Task<Initiative> GetById(int id);
        Task SaveInitiative(Initiative initiative);





        //pendiente de implementar
        Task DeleteInitiative(int id);

    }
}
