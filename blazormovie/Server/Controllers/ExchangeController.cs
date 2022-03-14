using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;

namespace blazormovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeRepository _exchangeRepository;
        public ExchangeController(IExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;   
        }
        [HttpGet]
        public async Task<IEnumerable<Exchanges>> Get()
        {
            var data = await _exchangeRepository.GetAll();
            return data;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Exchanges exchange)
        {
            if (exchange == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _exchangeRepository.Insert(exchange);

                scope.Complete();
            }
            return NoContent();
        }
        [HttpGet("GetByPoId/{id}")]
        public async Task<Exchanges> GetByPoId(int id)
        {
            Exchanges oExchange = new Exchanges();
            var data = await _exchangeRepository.GetByPoId(id);
            if (data != null)
            {
                oExchange = data.FirstOrDefault();
            }
            return oExchange;

        }

    }
}
