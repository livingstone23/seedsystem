using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;

namespace blazormovie.repository.Repository.ModBudget
{
    public class GroupsRepository: IGroupsRepository
    {

        private readonly IDbConnection _dbConnection;

        public GroupsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            var sql = @"Select Id, Name,  Description from Groups order by Id";

            return await _dbConnection.QueryAsync<Group>(sql, new { });
        }

        public async Task<PagingResponseModel<List<Group>>> GetGroupsByPagination(int currentPageNumber, int pageSize)
        {

            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM Groups

                       Select a.Id, a.Name, isNull(a.Description,'') as Description
                        from Groups a
                        order by a.Id Desc
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY"

                        ;

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<Group> allTodos = reader.Read<Group>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<Group>>(allTodos, count, currentPageNumber, pageSize);
            return result;

        }

        public async Task<List<Group>> GetByClientId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> GetById(int id)
        {
            var sql = @"Select Id, Name, isNull(Description,'') as Description
                        From Groups
                        Where  Id = @Id ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Group>(sql, new { Id = id });
        }

        public async Task<bool> Insert(Group group)
        {
            var sql = @" INSERT INTO   Groups( Name,  Description) 
                                       Values(@Name, @Description)";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    Name = group.Name,
                    Description = group.Description
                });

            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @" Delete from Groups Where Id = @Id  ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<bool> Update(Group group)
        {
            var sql = @" 
                        Update Groups
                            Set Name = @Name,
                                Description = @Description
                            Where Id = @Id ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    group.Name,
                    group.Description,
                    group.Id
                });

            return result > 0;
        }

        public async Task<IEnumerable<Initiative>> GetInitiativeByGroup(int GroupId)
        {
            var sql = @" Select a.Id, a.Name, a.Description
                        from Initiative a
                        inner join InitiativeGroups b  on a.Id = b.InitiativeId
                        Where GroupsId = @Id ";
            return await _dbConnection.QueryAsync<Initiative>(sql, new { Id = GroupId });
        }

        public async Task<bool> DeleteInitiativeGroup(int InitiativeId)
        {
            var sql = @"DELETE FROM InitiativeGroups 
                        WHERE InitiativeId = @Id ";
            var result = await _dbConnection.ExecuteAsync(sql,
                new { Id = InitiativeId });

            return result > 0;
        }
    }
}
