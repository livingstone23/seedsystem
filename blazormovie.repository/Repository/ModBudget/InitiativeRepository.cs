using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                

        public async Task<IEnumerable<Initiative>> GetAll()
        {
            var sql = @"Select Id, Name,  Description from Initiative order by Id";

            return await _dbConnection.QueryAsync<Initiative>(sql, new { });
        }


        public async Task<PagingResponseModel<List<Initiative>>> GetInitiativesByPagination(int currentPageNumber, int pageSize)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM Initiative

                       Select a.Id, a.Name, a.Description, Count(b.IdInitiative) as totalProjects
                        from Initiative a
                        left join dbo.Project b on a.Id = b.IdInitiative
                        group by a.id, a.Name, a.Description
                        order by a.Id Desc

                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<Initiative> allTodos = reader.Read<Initiative>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<Initiative>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }

        public async Task<PagingResponseModel<List<Initiative>>> GetInitiativesByPaginationAndGroupId(int currentPageNumber, int pageSize, int groupId)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM Initiative a
                        inner join [dbo].[InitiativeGroups] b on a.Id = b.InitiativeId
                        Where b.GroupsId = @groupId

                        Select a.Id, a.Name, a.Description, Count(b.IdInitiative) as totalProjects
                        from Initiative a
                        left join dbo.Project b on a.Id = b.IdInitiative
                        inner join [dbo].[InitiativeGroups] c on a.Id = c.InitiativeId
                        Where c.GroupsId = @groupId
                        group by a.id, a.Name, a.Description
                        order by a.Id Desc

                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take, groupId = groupId });

            int count = reader.Read<int>().FirstOrDefault();
            List<Initiative> allTodos = reader.Read<Initiative>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<Initiative>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }




        public async Task<Initiative> GetById(int id)
        {
            var sql = @"Select Id, Name,  isNull(Description,'') as Description 
                        From Initiative
                        Where  Id = @Id ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Initiative>(sql, new { Id = id });
        }


        public async Task<bool> Insert(Initiative initiative)
        {
            var sql = @" INSERT INTO   Initiative( Name,  Description) 
                                           Values(@Name, @Description)";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    Name = initiative.Name,
                    Description = initiative.Description
                });

            return result > 0;
        }


        public async Task<bool> Update(Initiative initiative)
        {
            var sql = @" 
                        Update Initiative
                            Set Name = @Name,
                                Description = @Description
                            Where Id = @Id ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    initiative.Name,
                    initiative.Description,
                    initiative.Id
                });

            return result > 0;
        }


        public async Task<bool> Delete(int id)
        {
            var sql = @" Delete from Initiative Where Id = @Id  ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;

        }

    }
}
