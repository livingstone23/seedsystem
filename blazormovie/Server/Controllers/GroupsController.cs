using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace blazormovie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    public class GroupsController : ControllerBase
    {


        private readonly IGroupsRepository _groupsRepository;
        private readonly IInitiaiveRepository _initiaiveRepository;

        public GroupsController(IGroupsRepository groupRepository,IInitiaiveRepository initiaiveRepository)
        {
            _groupsRepository = groupRepository;
            _initiaiveRepository = initiaiveRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Group>> Get()
        {
            var groups = await _groupsRepository.GetAll();
            
            return groups;
        }


        [HttpGet("GetByPagination/{currentPageNumber}/{pageSize}")]
        public async Task<PagingResponseModel<List<Group>>> GetByPagination(int currentPageNumber, int pageSize)
        {
            return await _groupsRepository.GetGroupsByPagination(currentPageNumber, pageSize);
        }

        [HttpGet("GetByPaginationAndClientId/{currentPageNumber}/{pageSize}/{clientId}")]
        public async Task<PagingResponseModel<List<Group>>> GetByPaginationAndClientId(int currentPageNumber, int pageSize, int clientId)
        {
            return await _groupsRepository.GetGroupsByPaginationAndClientId(currentPageNumber, pageSize, clientId);
        }


        [HttpGet("GetById/{id}")]
        public async Task<Group> GetById(int id)
        {

            var group = await _groupsRepository.GetById(id);
            var initiatives = await _groupsRepository.GetInitiativeByGroup(group.Id);
            if (group != null)
                group.Initiatives = initiatives.ToList();
            return group;

        }
        [HttpGet("GetInitiativeGroupsById/{id}")]
        public async Task<InitiativeGroup> GetInitiativeGroupsById(int id)
        { 
            InitiativeGroup initiativeGroup = new InitiativeGroup();
            var data = await _groupsRepository.GetInitiativesGroups(id);
            if (data != null)
                initiativeGroup = data.FirstOrDefault();
            return initiativeGroup;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Group group)
        {
            if (group == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _groupsRepository.Insert(group);

                scope.Complete();
            }

            return NoContent();
        }
        //[HttpPost("InsertInitiative/{initiativeId}/{groupId}")]
        //public async Task<IActionResult> InsertInitiative(int initiativeId, int groupId)
        //{
           
        //    //comprobamos que exista la iniciativa
        //    var ini = _initiaiveRepository.GetById(initiativeId);
        //    if(ini.Result != null)
        //    {
        //        //Comprobamos si la iniciativa está asociada a otro grupo
        //        var data = _groupsRepository.GetInitiativesGroups(initiativeId);

        //        if (!data.Result.Any())
        //        {

        //            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //            {
        //                await _groupsRepository.InsertInitiative(initiativeId, groupId);
        //                scope.Complete();
        //            }
        //        }
        //    }
           
        //    return NoContent();
           
        //}

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _groupsRepository.Delete(id);
        }

        [HttpDelete("DeleteInitiativeGroupById/{id}")]
        public async Task  DeleteIniByGroup(int id)
        {
            await _groupsRepository.DeleteInitiativeGroup(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Group group)
        {
            
            if (group == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                List<InitiativeGroup> ltIniGroup = new List<InitiativeGroup>();
                await _groupsRepository.Update(group);
                await _groupsRepository.DeleteInitiativeGroup(group.Id); 

                if(group.Initiatives != null && group.Initiatives.Any())
                {
                    foreach(var initiative in group.Initiatives)
                    {
                        //comprobamos que las iniciativas no estén asociadas a otro grupo de trabajo
                        InitiativeGroup iniGroup = new InitiativeGroup();
                        iniGroup = await GetInitiativeGroupsById(initiative.Id);
                        if (iniGroup == null)
                        {
                            await _groupsRepository.InsertInitiative(group.Id, initiative);
                        }
                     
                    }
                }
                scope.Complete();
               
            }
          
            return NoContent();
        }


    }
}
