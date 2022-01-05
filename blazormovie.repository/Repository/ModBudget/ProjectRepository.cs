using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<IEnumerable<Project>> GetProjects()
        {
            var sql = @"Select Id, Name, Description, AmountDefined, IdInitiative from Project;";

            return await _dbConnection.QueryAsync<Project>(sql, new { });

        }

        public async Task<PagingResponseModel<List<Project>>> GetProjectByPagination(int currentPageNumber, int pageSize)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM Project

                        Select Id, Name, Description, AmountDefined, IdInitiative 
                        from Project
                        ORDER BY Id
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<Project> allTodos = reader.Read<Project>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<Project>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }


            public async Task<Project> GetById(int id)
        {
            var sql = @" Select Id, Name, Description, AmountDefined, IdInitiative 
                         From Project
                         Where  Id = @Id     ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Project>(sql, new { Id = id });

        }

        public async  Task<IEnumerable<Project>> GetByInitiative(int initiativeid)
        {
            var sql = @"Select Id, Name, Description, AmountDefined, IdInitiative 
                        From Project
                        Where  IdInitiative = @Id ";


            return await _dbConnection.QueryAsync<Project>(sql, new { Id = initiativeid  });
        }

        public async Task<bool> SaveProject(Project project)
        {
            var sql = @" INSERT INTO Project (  Name,  Description,  AmountDefined,  IdInitiative) 
                                      VALUES ( @Name, @Description, @AmountDefined, @IdInitiative) ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    Name = project.Name,
                    Description = project.Description,  
                    AmountDefined= project.AmountDefined,
                    IdInitiative = project.IdInitiative
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

        public async Task<bool> Delete(int id)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;

        }
    }
}
