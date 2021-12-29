using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class ProjectWithInitiativeRepository : IProjectWithInitiativeRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProjectWithInitiativeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<ProjectWithInitiative>> GetAll()
        {

            var sql = @"";

            return await _dbConnection.QueryAsync<ProjectWithInitiative>(sql, new { });

        }
    }
}
