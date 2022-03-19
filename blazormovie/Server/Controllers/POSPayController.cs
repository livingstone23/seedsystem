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
    public class POSPayController : ControllerBase
    {

        private readonly IPOSPayRepository _pOSPayRepository;
        private readonly IExchangeRepository _exchangeRepository;
        public POSPayController(IPOSPayRepository pOSPayRepository,IExchangeRepository exchangeRepository)
        {
            _pOSPayRepository = pOSPayRepository;
            _exchangeRepository = exchangeRepository;
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
                bool bCambio=false;
                Exchanges exchange = new Exchanges();
                if (pOSPay.Exchange != 0)
                {
                    bCambio = true;
                    //obtenemos el último id y le sumamos uno
                    var data = _pOSPayRepository.GetPOSPays().Result;
                    List<POSPay> ltpos = (List<POSPay>)data;

                    int idPos = (from e in ltpos orderby e.Id descending select e.Id).FirstOrDefault() +1;

                    ltpos = null;
                    ////guardamos el cambio de moneda
                   
                    exchange.Exchange = pOSPay.Exchange;
                    exchange.Pounds = pOSPay.PayAmount;
                    exchange.IdPo = idPos;

                    ////hacemos la conversión
                    pOSPay.PayAmount = pOSPay.PayAmount * pOSPay.Exchange;
                    //guardamos exchange
                    
                }
                await _pOSPayRepository.SavePOSPay(pOSPay);
                if (bCambio)
                {
                    await _exchangeRepository.Insert(exchange);
                }
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
