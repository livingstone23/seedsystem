using blazormovie.repository.Interface.ModBudget;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;

namespace blazormovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class FacturaController:ControllerBase
    {
        private readonly IFacturaRepository _facturaRepository;
        public FacturaController(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Shared.SeedEntities.Factura factura)
        {
            if (factura == null)
                return BadRequest();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _facturaRepository.Insert(factura);
                scope.Complete();
            }
            return NoContent();
        }
    }
}
