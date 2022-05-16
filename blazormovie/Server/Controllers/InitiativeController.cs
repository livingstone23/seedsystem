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


        [HttpGet("GetByPagination/{currentPageNumber}/{pageSize}")]
        public async Task<PagingResponseModel<List<Initiative>>> GetByPagination(int currentPageNumber, int pageSize)
        {
            return await _initiativeRepository.GetInitiativesByPagination(currentPageNumber, pageSize);
        }

        [HttpGet("GetByPaginationAndGroupId/{currentPageNumber}/{pageSize}/{groupId}")]
        public async Task<PagingResponseModel<List<Initiative>>> GetByPaginationAndGroupId(int currentPageNumber, int pageSize, int groupId)
        {
            return await _initiativeRepository.GetInitiativesByPaginationAndGroupId(currentPageNumber, pageSize, groupId);
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


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _initiativeRepository.Delete(id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Initiative initiative)
        {

            if (initiative == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _initiativeRepository.Update(initiative);

                scope.Complete();
            }

            return NoContent();
        }
        [HttpGet("GetInitiativeGroups")]
        public async Task<IEnumerable<InitiativeGroup>> GetInitiativeGroups()
        {
            var initiativeGroups = await _initiativeRepository.GetInitiativeGroups();

            return initiativeGroups;
        }

    }

}
