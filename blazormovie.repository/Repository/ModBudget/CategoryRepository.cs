using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly IDbConnection _dbConnection;

        public CategoryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var sql = @"Select Id, Name,  Description from Category;";

            return await _dbConnection.QueryAsync<Category>(sql, new { });
        }


        public async Task<bool> Delete(int id)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        

        public async Task<Category> GetById(int id)
        {

            var sql = @"    ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });

        }

        public async Task<bool> Insert(Category category)
        {
            
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {

                });

            return result > 0;
        }

        public async Task<bool> Update(Category category)
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
