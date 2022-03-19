using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class CostRepository : ICostRepository
    {
        private readonly IDbConnection _dbConnection;

        public CostRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cost>> GetAll()
        {
            var sql = @"Select Id, Name,Description from Cost order by Id";

            return await _dbConnection.QueryAsync<Cost>(sql, new { });
        }

        public async Task<Cost> GetById(int id)
        {
            var sql = @"SELECT Id,Name,Description FROM Cost
                        WHERE Id = @id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Cost>(sql, new { Id=id });
        }

        public async Task<PagingResponseModel<List<Cost>>> GetCostsByPagination(int currentPageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(Cost cost)
        {
           
            var sql = @" INSERT INTO   Cost( Name,  Description) 
                                    Values(@Name, @Description)";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    Name = cost.Name,
                    Description = cost.Description,
                    
                });

            return result > 0;
            
        }

        public Task<bool> Update(Cost cost)
        {
            throw new NotImplementedException();
        }
    }
}
