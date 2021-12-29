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





    }
}
