using blazormovie.Shared.SeedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IInitiaiveRepository
    {

        Task<IEnumerable<Initiative>> GetAll();

        Task<PagingResponseModel<List<Initiative>>> GetInitiativesByPagination(int currentPageNumber, int pageSize);
        Task<Initiative> GetById(int id);

        Task<bool> Insert(Initiative initiative);

        Task<bool> Delete(int id);
        Task<bool> Update(Initiative initiative);

    }
}
