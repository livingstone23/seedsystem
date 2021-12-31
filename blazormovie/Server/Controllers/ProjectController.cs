using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeedSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await _projectRepository.GetAll();
        }


        [HttpGet("GetByInitiative/{initiativeId}")]
        public async Task<IEnumerable<Project>> GetByInitiative(int initiativeId)
        {
            return await _projectRepository.GetByInitiative(initiativeId);
        }



    }

}
