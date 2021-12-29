using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class InitiativeRepository : IInitiaiveRepository
    {

        private readonly IDbConnection _dbConnection;

        public InitiativeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Initiative>> GetAll()
        {
            var sql = @"Select Id, Name,  Description from Initiative";

            return await _dbConnection.QueryAsync<Initiative>(sql, new { });
        }

        public async Task<Initiative> GetById(int id)
        {
            var sql = @"    ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Initiative>(sql, new { Id = id });
        }

        public async Task<bool> Insert(Initiative initiative)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {

                });

            return result > 0;
        }

        public async Task<bool> Update(Initiative initiative)
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
