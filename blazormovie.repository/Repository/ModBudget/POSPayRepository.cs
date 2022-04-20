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
                        ORDER BY Id Desc
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<POSPay> allTodos = reader.Read<POSPay>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<POSPay>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }


        public async Task<PagingResponseModel<List<POSPayDTO>>> GetPOSPayDtosByPagination(int currentPageNumber, int pageSize)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM POSPay

                        Select a.Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, a.IdProject, a.IdInitiative, IdPOSPaysAdjust, b.name as ProjectName, c.name as InitiativeName 
                        from POSPay a
                        left join Project b   on a.IdProject  = b.Id
                        left join Initiative c on a.IdInitiative  = c.Id
                        ORDER BY a.Id Desc
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<POSPayDTO> allTodos = reader.Read<POSPayDTO>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<POSPayDTO>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }


        public async Task<PagingResponseModel<List<POSPayDTO>>> GetPOSPayDtosByPaginationAndProjectId(int currentPageNumber, int pageSize, int projectId)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"SELECT 
                        COUNT(*)
                        FROM POSPay Where IdProject = @projectId

                        Select a.Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, a.IdProject, a.IdInitiative, IdPOSPaysAdjust, b.name as ProjectName, c.name as InitiativeName 
                        from POSPay a
                        left join Project b   on a.IdProject  = b.Id
                        left join Initiative c on a.IdInitiative  = c.Id
                        Where a.IdProject = @projectId
                        ORDER BY a.Id Desc
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take, projectId = projectId });

            int count = reader.Read<int>().FirstOrDefault();
            List<POSPayDTO> allTodos = reader.Read<POSPayDTO>().ToList();

            //return await _dbConnection.QueryAsync<POSPay>(sql, new { });
            var result = new PagingResponseModel<List<POSPayDTO>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }


        public async Task<POSPay> GetById(int id)
        {
            var sql = @"Select Id, PayDay, CurrencyPay, DescriptionPOS, NumberTransfer, PayAmount, RateChange, IdProject, IdInitiative, IdPOSPaysAdjust, profile_pic_data_url, profile_pdf_data_url 
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
            var sql = @" INSERT INTO POSPay(  PayDay,  CurrencyPay,  DescriptionPOS,  NumberTransfer,  PayAmount,  RateChange,  IdInitiative,  IdProject,  IdPOSPaysAdjust,  profile_pic_data_url,  profile_pdf_data_url)
                                    values ( @PayDay, @CurrencyPay, @DescriptionPOS, @NumberTransfer, @PayAmount, @RateChange, @IdInitiative, @IdProject, @IdPOSPaysAdjust, @profile_pic_data_url, @profile_pdf_data_url ) ";

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
                    IdPOSPaysAdjust = posPay.IdPOSPaysAdjust,
                    profile_pic_data_url = posPay.profile_pic_data_url,
                    profile_pdf_data_url = posPay.profile_pdf_data_url,
                });

            return result > 0;
        }


        public async Task<bool> Update(POSPay posPay)
        {
            var sql = @" 
                        Update POSPay
                            Set PayDay = @PayDay,
                                CurrencyPay = @CurrencyPay,
                                DescriptionPOS = @DescriptionPOS,
                                NumberTransfer = @NumberTransfer,
                                PayAmount = @PayAmount,
                                RateChange = @RateChange,
                                IdProject = @IdProject,
                                IdInitiative = @IdInitiative,
                                IdPOSPaysAdjust = @IdPOSPaysAdjust,
                                profile_pic_data_url = @profile_pic_data_url,
                                profile_pdf_data_url = @profile_pdf_data_url
                        Where   Id = @Id ";

            var result = await _dbConnection.ExecuteAsync(sql,
                new
                {
                    posPay.PayDay,
                    posPay.CurrencyPay,
                    posPay.DescriptionPOS,
                    posPay.NumberTransfer,
                    posPay.PayAmount,
                    posPay.RateChange,
                    posPay.IdProject,
                    posPay.IdInitiative,
                    posPay.IdPOSPaysAdjust,
                    posPay.profile_pic_data_url,
                    posPay.profile_pdf_data_url,
                    posPay.Id
                });

            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @" Delete from  POSPay Where Id = @Id ";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        
    }
}
