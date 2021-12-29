using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class POSPayRepository : IPOSPayRepository
    {

        private readonly IDbConnection _dbConnection;

        public POSPayRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        public async Task<IEnumerable<POSPay>> GetAll()
        {
            var sql = @"Select Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, IdProject, IdInitiative, IdPOSPaysAdjust from POSPay";

            return await _dbConnection.QueryAsync<POSPay>(sql, new { });
        }

        public async Task<POSPay> GetById(int id)
        {
            var sql = @"    ";

            return await _dbConnection.QueryFirstOrDefaultAsync<POSPay>(sql, new { Id = id });
        }

        public async Task<bool> Insert(POSPay posPay)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {

                });

            return result > 0;
        }

        public async Task<bool> Update(POSPay posPay)
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
