using Microsoft.AspNetCore.Mvc;
using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitiativeController : ControllerBase
    {
        private readonly IInitiaiveRepository _initiativeRepository;

        public InitiativeController(IInitiaiveRepository initiativeRepository)
        {
            _initiativeRepository = initiativeRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Initiative>> Get()
        {
            return await _initiativeRepository.GetAll();
        }



    }

}
