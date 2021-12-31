using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProjectRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;

        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            var sql = @"Select Id, Name, Description, AmountDefined, IdInitiative from Project;";

            return await _dbConnection.QueryAsync<Project>(sql, new { });

        }

        public async  Task<IEnumerable<Project>> GetByInitiative(int initiativeid)
        {
            var sql = @"Select Id, Name, Description, AmountDefined, IdInitiative 
                        From Project
                        Where  IdInitiative = @Id ";


            return await _dbConnection.QueryAsync<Project>(sql, new { Id = initiativeid  });
        }

        public async Task<Project> GetById(int id)
        {
            var sql = @"    ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Project>(sql, new { Id = id });

        }

        

        public async Task<bool> Insert(Project project)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {

                });

            return result > 0;

        }

        public async Task<bool> Update(Project project)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {

                });

            return result > 0;

        }
    }
}
