using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
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
