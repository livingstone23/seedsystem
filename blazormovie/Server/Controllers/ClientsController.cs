using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace blazormovie.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class ClientsController : ControllerBase
    {

        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        

        [HttpGet]
        public async Task<IEnumerable<Shared.SeedEntities.Client>> Get()
        {
            var clients = await _clientRepository.GetAll();

            return clients;
        }


        [HttpGet("GetByPagination/{currentPageNumber}/{pageSize}")]
        public async Task<PagingResponseModel<List<Shared.SeedEntities.Client>>> GetByPagination(int currentPageNumber, int pageSize)
        {
            return await _clientRepository.GetClientsByPagination(currentPageNumber, pageSize);
        }


        [HttpGet("GetById/{id}")]
        public async Task<Shared.SeedEntities.Client> GetById(int id)
        {

            var client = await _clientRepository.GetById(id);

            return client;

        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Shared.SeedEntities.Client client)
        {
            if (client == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _clientRepository.Insert(client);

                scope.Complete();
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _clientRepository.Delete(id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Shared.SeedEntities.Client client)
        {

            if (client == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _clientRepository.Update(client);

                scope.Complete();
            }

            return NoContent();
        }

    }
}
