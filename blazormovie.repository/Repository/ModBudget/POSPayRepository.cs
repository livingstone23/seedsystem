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
    public class POSPayRepository : IPOSPayRepository
    {

        private readonly IDbConnection _dbConnection;

        public POSPayRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        
        public async Task<IEnumerable<POSPay>> GetPOSPays()
        {
            var sql = @"Select Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, IdProject, IdInitiative, IdPOSPaysAdjust from POSPay";

            return await _dbConnection.QueryAsync<POSPay>(sql, new { });
        }

        public async Task <PagingResponseModel<List<POSPay>>> GetPOSPaysByPagination(int currentPageNumber, int pageSize)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM POSPay

                        Select Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, IdProject, IdInitiative, IdPOSPaysAdjust 
                        from POSPay
                        ORDER BY Id
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<POSPay> allTodos = reader.Read<POSPay>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<POSPay>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }

        //Task<PagingResponseModel<List<POSPay>>> IPOSPayRepository.GetPOSPaysByPagination(int currentPageNumber, int pageSize)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<POSPay> GetById(int id)
        {
            var sql = @"Select Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, IdProject, IdInitiative, IdPOSPaysAdjust 
                        from POSPay Where Id = @Id ";

            return await _dbConnection.QueryFirstOrDefaultAsync<POSPay>(sql, new { Id = id });
        }


        public async Task<IEnumerable<POSPay>> GetByInitiative(int initiativeId)
        {
            var sql = @"Select Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, IdProject, IdInitiative, IdPOSPaysAdjust 
                        from POSPay Where IdInitiative = @Id ";

            return await _dbConnection.QueryAsync<POSPay>(sql, new { Id = initiativeId });
        }


        public async Task<IEnumerable<POSPay>> GetByProject(int projectId)
        {
            var sql = @"Select Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, IdProject, IdInitiative, IdPOSPaysAdjust 
                        from POSPay Where  IdProject = @Id ";

            return await _dbConnection.QueryAsync<POSPay>(sql, new { Id = projectId });
        }


        public async Task<bool> SavePOSPay(POSPay posPay)
        {
            var sql = @" INSERT INTO POSPay(  PayDay,  CurrencyPay,  DescriptionPOS,  NumberTransfer,  PayAmount,  RateChange,  IdInitiative,  IdProject,  IdPOSPaysAdjust)
                                    values ( @PayDay, @CurrencyPay, @DescriptionPOS, @NumberTransfer, @PayAmount, @RateChange, @IdInitiative, @IdProject, @IdPOSPaysAdjust) ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    PayDay = posPay.PayDay,
                    CurrencyPay = posPay.CurrencyPay,
                    DescriptionPOS = posPay.DescriptionPOS,
                    NumberTransfer = posPay.NumberTransfer,
                    PayAmount = posPay.PayAmount,
                    RateChange = posPay.RateChange,
                    IdInitiative = posPay.IdInitiative,
                    IdProject = posPay.IdProject,
                    IdPOSPaysAdjust = posPay.IdPOSPaysAdjust
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

        public async Task<bool> Delete(int id)
        {
            var sql = @"   ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        
    }
}
