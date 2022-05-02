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

            //var sql = @"SELECT 
            //            COUNT(*)
            //            FROM Project

            //            Select a.id, a.Name, a.Description, a.AmountDefined, a.IdInitiative, Count(b.IdProject) as TotalPays
            //            from dbo.Project a
            //            left join dbo.POSPay b on a.id = b.IdProject
            //            group by a.id, a.Name, a.Description, a.AmountDefined, a.IdInitiative, b.IdProject
            //            order by a.Id
            //            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";            

            var sql = @"SELECT 
	                    COUNT(*)
	                    FROM Project

	                    Select a.id, a.Name, isnull(a.Description, '') as Description , a.AmountDefined, a.IdInitiative,ini.Name as InitiativeName, Count(b.IdProject) as TotalPays
	                    from dbo.Project a
	                    left join dbo.POSPay b on a.id = b.IdProject
	                    left join dbo.Initiative ini on a.IdInitiative = ini.Id
	                    group by a.id, a.Name, a.Description, a.AmountDefined, a.IdInitiative, b.IdProject, ini.Name
	                    order by a.Id Desc
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<Project> allTodos = reader.Read<Project>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<Project>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }


        public async Task<PagingResponseModel<List<Project>>> GetProjectByPaginationAndInitiativeId(int currentPageNumber, int pageSize, int initiativeId)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            //var sql = @"SELECT 
            //            COUNT(*)
            //            FROM Project

            //            Select a.id, a.Name, a.Description, a.AmountDefined, a.IdInitiative, Count(b.IdProject) as TotalPays
            //            from dbo.Project a
            //            left join dbo.POSPay b on a.id = b.IdProject
            //            group by a.id, a.Name, a.Description, a.AmountDefined, a.IdInitiative, b.IdProject
            //            order by a.Id
            //            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";            

            var sql = @"SELECT 
	                    COUNT(*)
	                    FROM Project
                        Where IdInitiative = @initiativeId

	                    Select a.id, a.Name, isnull(a.Description, '') as Description , a.AmountDefined, a.IdInitiative,ini.Name as InitiativeName, Count(b.IdProject) as TotalPays
	                    from dbo.Project a
	                    left join dbo.POSPay b on a.id = b.IdProject
	                    left join dbo.Initiative ini on a.IdInitiative = ini.Id
                        Where a.IdInitiative = @initiativeId
	                    group by a.id, a.Name, a.Description, a.AmountDefined, a.IdInitiative, b.IdProject, ini.Name
	                    order by a.Id Desc
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take, initiativeId = initiativeId });

            int count = reader.Read<int>().FirstOrDefault();
            List<Project> allTodos = reader.Read<Project>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<Project>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }


        public async Task<Project> GetById(int id)
        {
            var sql = @" Select Id, Name, isnull(Description,'') as Description, AmountDefined, IdInitiative 
                         From Project
                         Where  Id = @Id     ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Project>(sql, new { Id = id });

        }

        public async  Task<IEnumerable<Project>> GetByInitiative(int initiativeid)
        {
            var sql = @"Select Id, Name, isnull(Description,'') as Description, AmountDefined, IdInitiative 
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
            try
            {
                var sql = @" 
                        Update Project
                            Set Name = @Name,
                                Description = @Description,
                                AmountDefined = @AmountDefined,
                                IdInitiative = @IdInitiative
                           Where Id = @Id ";

                var result = await _dbConnection.ExecuteAsync(sql,
                    new
                    {
                        Name = project.Name,
                        Description =project.Description,
                        AmountDefined = project.AmountDefined,
                        IdInitiative = project.IdInitiative,
                        Id = project.Id
                    });

                return result > 0;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }

        }


        public async Task<bool> Delete(int id)
        {
            var sql = @" Delete from Project Where  Id = @Id ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;

        }

        public async Task<bool> InsertProjectCost(int projectId, ProjectCost cost)
        {
            var sql = @"INSERT INTO [dbo].[ProjectCost]( ProjectId,  CostId, DateOfCost,amount)
                                          VALUES(@projectId, @costId, @DateOfCost, @Amount)";


            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    projectId = projectId,
                    costId = cost.CostId,
                    DateOfCost = DateTime.Now,
                    Amount = cost.Amount
                });

            return result > 0;
        }

        public async Task<bool> DeleteProjectCost(int projectCostId)
        {
            var sql = @"DELETE FROM ProjectCost 
                        WHERE Id = @Id ";
            var result = await _dbConnection.ExecuteAsync(sql,
                new { Id = projectCostId });

            return result > 0;
        }

        public async Task<IEnumerable<ProjectCost>> GetCostByProject(int projectId)
        {
            var sql = @" Select b.Id, a.Name, a.Description,b.Amount, b.DateOfCost,b.ProjectId,b.CostId
                        from Cost a
                        inner join ProjectCost b on a.Id = b.CostId
                        Where b.ProjectId = @Id ";
            return await _dbConnection.QueryAsync<ProjectCost>(sql, new { Id = projectId });
        }
    }
}
