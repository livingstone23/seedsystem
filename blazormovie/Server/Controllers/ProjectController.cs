using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IPOSPayRepository _pOSPayRepository;


        public ProjectController(IProjectRepository projectRepository, IPOSPayRepository pOSPayRepository)
        {
            _projectRepository = projectRepository;
            _pOSPayRepository = pOSPayRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            var projects = await _projectRepository.GetProjects();
            foreach (var item in projects)
            {
                item.POSPays = (List<POSPay>)await _pOSPayRepository.GetByProject(item.Id);
            }
            return projects;
        }


        [HttpGet("GetByPagination/{currentPageNumber}/{pageSize}")]
        public async Task<PagingResponseModel<List<Project>>> GetByPagination(int currentPageNumber, int pageSize)
        {
            var result = await _projectRepository.GetProjectByPagination(currentPageNumber, pageSize);


            return result;
        }

        [HttpGet("GetByPaginationAndInitiativeId/{currentPageNumber}/{pageSize}/{initiativeId}")]
        public async Task<PagingResponseModel<List<Project>>> GetByPaginationAndInitiativeId(int currentPageNumber, int pageSize, int initiativeId)
        {
            var result = await _projectRepository.GetProjectByPaginationAndInitiativeId(currentPageNumber, pageSize, initiativeId);


            return result;
        }


        [HttpGet("GetById/{id}")]
        public async Task<Project> GetById(int id)
        {
            var project = await _projectRepository.GetById(id);
            var POSPays = await _pOSPayRepository.GetByProject(project.Id);
            var cost = await _projectRepository.GetCostByProject(project.Id);
            if (project != null && POSPays != null)
            {
                project.POSPays = POSPays.ToList();
            }
            if (project != null && cost != null)
            {
                project.Costs = cost.ToList();
            }
            return project;
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


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _projectRepository.Delete(id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Project project)
        {

            if (project == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _projectRepository.Update(project);

                scope.Complete();
            }

            return NoContent();
        }
    }

}
