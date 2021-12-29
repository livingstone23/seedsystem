using Microsoft.AspNetCore.Mvc;
using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return await _pOSPayRepository.GetAll();
        }



    }

}
