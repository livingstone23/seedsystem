using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class ClientRepository : IClientRepository
    {

        private readonly IDbConnection _dbConnection;
        
        public ClientRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<IEnumerable<Client>> GetAll()
        {
            var sql = @"Select Id, Name,  Description from Client order by Id";

            return await _dbConnection.QueryAsync<Client>(sql, new { });
        }

        public async Task<PagingResponseModel<List<Client>>> GetClientsByPagination(int currentPageNumber, int pageSize)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM Client

                       Select a.Id, a.Name, isNull(a.Description,'') as Description
                        from Client a
                        order by a.Id Desc
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<Client> allTodos = reader.Read<Client>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<Client>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }

        public async Task<Client> GetById(int id)
        {
            var sql = @"Select Id, Name, isNull(Description,'')
                        From Client
                        Where  Id = @Id ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id });
        }

        public async Task<bool> Insert(Client client)
        {
            var sql = @" INSERT INTO   Client( Name,  Description) 
                                       Values(@Name, @Description)";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    Name = client.Name,
                    Description = client.Description
                });

            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @" Delete from Client Where Id = @Id  ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<bool> Update(Client client)
        {
            var sql = @" 
                        Update Client
                            Set Name = @Name,
                                Description = @Description
                            Where Id = @Id ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    client.Name,
                    client.Description,
                    client.Id
                });

            return result > 0;
        }
    }
}
