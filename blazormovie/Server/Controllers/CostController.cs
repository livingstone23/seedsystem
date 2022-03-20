using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace blazormovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class CostController : ControllerBase
    {
        private readonly ICostRepository _costRepository;
        public CostController(ICostRepository costRepository)
        {
            _costRepository = costRepository;   
        }
        [HttpGet]
        public async Task<IEnumerable<Cost>> Get() 
        {
            var costs = await _costRepository.GetAll();
            return costs;
        }
        [HttpGet("GetByPagination/{currentPageNumber}/{pageSize}")]
        public async Task<PagingResponseModel<List<Cost>>> GetByPagination(int currentPageNumber, int pageSize)
        {
            return await _costRepository.GetCostsByPagination(currentPageNumber, pageSize);
        }
        [HttpGet("GetById/{id}")]
        public async Task<Cost> GetById(int id)
        {
            var cost = await _costRepository.GetById(id);
            var projects = await _costRepository.GetProjectsByCost(id);
            if(cost != null)
            {
                cost.Projects = projects.ToList();
            }
           
            return cost;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cost cost)
        {
            if (cost == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _costRepository.Insert(cost);

                scope.Complete();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _costRepository.Delete(id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cost cost)
        {

            if (cost == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _costRepository.Update(cost);

                scope.Complete();
            }

            return NoContent();
        }
    }
}
