using Microsoft.AspNetCore.Mvc;
using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeController : ControllerBase
    {
        private readonly IInitiaiveRepository _initiativeRepository;
        private readonly IProjectRepository _projectRepository;

        public InitiativeController(IInitiaiveRepository initiativeRepository, IProjectRepository projectRepository)
        {
            _initiativeRepository = initiativeRepository;
            _projectRepository = projectRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Initiative>> Get()
        {
            var initiatives = await _initiativeRepository.GetAll();
            foreach (var item in initiatives)
            {
                item.Projects = (List<Project>)await _projectRepository.GetByInitiative(item.Id);
            }
            return initiatives;
        }


        [HttpGet("GetById/{id}")]
        public async Task<Initiative> GetById(int id)
        {
            var initiative = await _initiativeRepository.GetById(id);
            var projects = await _projectRepository.GetByInitiative(initiative.Id);
                
            if (initiative != null)
            {
                initiative.Projects = projects.ToList();
            }

            return initiative;
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
