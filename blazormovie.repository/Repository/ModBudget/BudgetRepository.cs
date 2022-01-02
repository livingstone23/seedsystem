using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly IDbConnection _dbConnection;

        public BudgetRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<IEnumerable<Budget>> GetAll()
        {
            var sql = "execute SP_BUDGET";

            return await _dbConnection.QueryAsync<Budget>(sql, new { });
        }

        //Task<IEnumerable<Budget>> IBudgetRepository.GetAll()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
