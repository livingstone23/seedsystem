using Microsoft.AspNetCore.Mvc;
using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeController : ControllerBase
    {
        private readonly IInitiaiveRepository _initiativeRepository;

        public InitiativeController(IInitiaiveRepository initiativeRepository)
        {
            _initiativeRepository = initiativeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Initiative>> Get()
        {
            return await _initiativeRepository.GetAll();
        }


        [HttpGet("GetById/{id}")]
        public async Task<Initiative> GetById(int id)
        {
            return await _initiativeRepository.GetById(id);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Initiative initiative)
        {
            if (initiative == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _initiativeRepository.Insert(initiative);

                scope.Complete();
            }

            return NoContent();
        }

    }

}
