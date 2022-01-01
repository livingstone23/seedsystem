using Microsoft.AspNetCore.Http;
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
    public class ProjectController : ControllerBase
    {

        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await _projectRepository.GetProjects();
        }


        [HttpGet("GetById/{id}")]
        public async Task<Project> GetById(int id)
        {
            return await _projectRepository.GetById(id);
        }


        [HttpGet("GetByInitiative/{initiativeId}")]
        public async Task<IEnumerable<Project>> GetByInitiative(int initiativeId)
        {
            return await _projectRepository.GetByInitiative(initiativeId);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            if (project == null)
                return BadRequest();

            if (project.IdInitiative == 0)
                ModelState.AddModelError("Initiative", "IdInitiative can't be empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _projectRepository.SaveProject(project);

                scope.Complete();
            }

            return NoContent();
        }



    }

}
