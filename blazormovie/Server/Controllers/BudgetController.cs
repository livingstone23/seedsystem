using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {

        private readonly IBudgetRepository _budgetRepository;

        public BudgetController(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Budget>> Get()
        {
            return await _budgetRepository.GetAll();
        }


        [HttpGet("GetByPaginationDto/{currentPageNumber}/{pageSize}")]
        public async Task<PagingResponseModel<List<BudgetDTO>>> GetByPaginationDto(int currentPageNumber, int pageSize)
        {
            return await _budgetRepository.GetBudgetDtosByPagination(currentPageNumber, pageSize);
        }




    }
}
