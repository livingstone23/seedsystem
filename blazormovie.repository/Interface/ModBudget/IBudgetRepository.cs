using blazormovie.Shared.SeedEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Interface.ModBudget
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetAll();
        Task<PagingResponseModel<List<BudgetDTO>>> GetBudgetDtosByPagination(int currentPageNumber, int pageSize);

        //copiar la vista

    }
}
