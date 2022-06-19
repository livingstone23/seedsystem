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

        public async Task<IEnumerable<Cost>> GetAll()
        {
            var sql = @"Select Id, Name,Description from Cost order by Id";

            return await _dbConnection.QueryAsync<Cost>(sql, new { });
        }

        public async Task<Cost> GetById(int id)
        {
            var sql = @"SELECT Id,Name,Description,Proveedor FROM Cost
                        WHERE Id = @id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Cost>(sql, new { Id=id });
        }

        public async Task<PagingResponseModel<List<Cost>>> GetCostsByPagination(int currentPageNumber, int pageSize)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM Cost

                       Select a.Id, a.Name, isNull(a.Description,'') as Description, isNull(a.Proveedor,'') as Proveedor
                        from Cost a
                        order by a.Id Desc
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<Cost> allTodos = reader.Read<Cost>().ToList();

            var result = new PagingResponseModel<List<Cost>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }

        public async Task<bool> Insert(Cost cost)
        {
           
            var sql = @" INSERT INTO   Cost( Name,  Description,Proveedor) 
                                    Values(@Name, @Description,@Proveedor)";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    Name = cost.Name,
                    Description = cost.Description,
                    Proveedor = cost.Proveedor
                });

            return result > 0;
            
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @" Delete from Cost Where Id = @Id  ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<bool> Update(Cost cost)
        {
            var sql = @" 
                        Update Cost
                            Set Name = @Name,
                                Description = @Description,
                                Proveedor = @Proveedor
                            Where Id = @Id ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    cost.Name,
                    cost.Description,
                    cost.Id,
                    cost.Proveedor
                });

            return result > 0;
        }

        public async Task<IEnumerable<Project>> GetProjectsByCost(int costId)
        {
            var sql = @"Select a.Id, a.Name, a.Description
                        from Project a
                        inner join ProjectCost b  on a.Id = b.ProjectId
                        Where b.CostId = @Id ";

            return await _dbConnection.QueryAsync<Project>(sql, new { Id = costId });
        }

        public async Task<IEnumerable<ProjectCost>> GetProjectCost()
        {
            // var sql = @"Select * from ProjectCost order by Id";
            var sql = @"Select a.Id,a.CostId,a.ProjectId,a.DateOfCost,a.amount,b.Name,b.Description from ProjectCost a
                        join cost b on a.CostId = b.Id order by a.Id";
            return await _dbConnection.QueryAsync<ProjectCost>(sql, new { });
        }
    }
}
