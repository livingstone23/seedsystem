using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.Entities;
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
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly IDbConnection _dbConnection;
        public ExchangeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @"DELETE FROM CambioDivisa WHERE Id = @id";
            
            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<Exchanges>> GetAll()
        {
            var sql = @"SELECT * FROM CambioDivisa ORDER BY Id";
            return await _dbConnection.QueryAsync<Exchanges>(sql, new { });
        }

        public async Task<IEnumerable<Exchanges>> GetByPoId(int poId)
        {
            var sql = @"SELECT * FROM CambioDivisa 
                        WHERE idPo = @poId";

            return await _dbConnection.QueryAsync<Exchanges>(sql,new { poId = poId });
            
        }

        public async Task<bool> Insert(Exchanges exchange)
        {
            var sql = @"INSERT INTO CambioDivisa (idPo,Pounds,Exchange)
                                    Values (@idPo,@Pounds,@Exchange)";
            var result = await _dbConnection.ExecuteAsync(sql,
                new { 
                    idPo = exchange.IdPo,
                    Pounds = exchange.Pounds,
                    Exchange = exchange.Exchange
                    });
            return result > 0;
        }

        public async Task<bool> Update(Exchanges exchange)
        {
            var sql = @"UPDATE CambioDivisa SET Exchange = @cambio, Pounds = @cantidad WHERE id = @idEx";
            var result = await _dbConnection.ExecuteAsync(sql,  
                new {
                        cambio = exchange.Exchange,
                        cantidad = exchange.Pounds,
                        idEx = exchange.Id
                    });
            
            return result > 0;
        }
    }
}
