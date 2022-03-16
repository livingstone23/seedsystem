using Microsoft.AspNetCore.Mvc;
using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class POSPayController : ControllerBase
    {

        private readonly IPOSPayRepository _pOSPayRepository;

        public POSPayController(IPOSPayRepository pOSPayRepository)
        {
            _pOSPayRepository = pOSPayRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<POSPay>> Get()
        {
            return await _pOSPayRepository.GetPOSPays();
        }


        [HttpGet("GetByPagination/{currentPageNumber}/{pageSize}")]
        public async Task<PagingResponseModel<List<POSPay>>> GetByPagination(int currentPageNumber, int pageSize)
        {
            return await _pOSPayRepository.GetPOSPaysByPagination(currentPageNumber, pageSize);
        }

        [HttpGet("GetByPaginationDto/{currentPageNumber}/{pageSize}")]
        public async Task<PagingResponseModel<List<POSPayDTO>>> GetByPaginationDto(int currentPageNumber, int pageSize)
        {
            return await _pOSPayRepository.GetPOSPayDtosByPagination(currentPageNumber, pageSize);
        }

        [HttpGet("GetByPaginationDtoAndProjectId/{currentPageNumber}/{pageSize}/{projectId}")]
        public async Task<PagingResponseModel<List<POSPayDTO>>> GetByPaginationDtoAndProjectId(int currentPageNumber, int pageSize, int projectId)
        {
            return await _pOSPayRepository.GetPOSPayDtosByPaginationAndProjectId(currentPageNumber, pageSize, projectId);
        }


        [HttpGet("GetById/{id}")]
        public async Task<POSPay> GetById(int id)
        {
            return await _pOSPayRepository.GetById(id);
        }


        [HttpGet("GetByInitiative/{initiativeId}")]
        public async Task<IEnumerable<POSPay>> GetByInitiative(int initiativeId)
        {
            return await _pOSPayRepository.GetByInitiative(initiativeId);
        }


        [HttpGet("GetByProject/{projectId}")]
        public async Task<IEnumerable<POSPay>> GetByProject(int projectId)
        {
            return await _pOSPayRepository.GetByProject(projectId);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] POSPay pOSPay)
        {
            if (pOSPay == null)
                return BadRequest();

            if (pOSPay.IdInitiative == 0 )
                ModelState.AddModelError("Order Number", "Order number can't be empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _pOSPayRepository.SavePOSPay(pOSPay);

                scope.Complete();
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _pOSPayRepository.Delete(id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] POSPay pOSPay)
        {

            if (pOSPay == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _pOSPayRepository.Update(pOSPay);

                scope.Complete();
            }

            return NoContent();
        }


    }

}
