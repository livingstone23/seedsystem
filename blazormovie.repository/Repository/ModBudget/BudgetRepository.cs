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
    public class BudgetRepository : IBudgetRepository
    {
        private readonly IDbConnection _dbConnection;

        public BudgetRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<IEnumerable<Budget>> GetAll()
        {
            var sql = "execute SP_BUDGET";

            return await _dbConnection.QueryAsync<Budget>(sql, new { });
        }


        public async Task<PagingResponseModel<List<BudgetDTO>>> GetBudgetDtosByPagination(int currentPageNumber, int pageSize)
        {
            int maxPagSize = 100;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var sql = @"Select Count(*)
                        from 
                        (
                        Select	a.Id as IdInitiative,
		                        a.Name as InitiativeName, 
		                        b.Id as IdProject,
		                        b.Name as NameProject, 
		                        isnull(rem.AmountInvoiced,0) as AmountInvoiced, 
		                        isnull(rem.POsReceived,0) as POsReceived, 
		                        ((isnull(rem.AmountInvoiced,0) - isnull(rem.POsReceived,0))*-1) as Balance, 
		                        isnull(rem.Adjustment,0) as Adjustment, 
		                        ((isnull(rem.AmountInvoiced,0) - isnull(rem.POsReceived,0) + isnull(rem.Adjustment,0))*-1) as FinalBalance 
                        from 
                        (Select IdInitiative, IdProject, AmountInvoiced, sum(PayAmount) as POsReceived, sum(Adjustment) as Adjustment   from 
                        (Select a.IdInitiative, a.IdProject, a.IdPOSPays, a.AmountInvoiced, a.PayAmount, b.PayAmount as Adjustment from
                        (Select 
                        c.IdInitiative,
                        c.Id as idProject,
                        a.Id as IdPosPays,
                        c.AmountDefined as AmountInvoiced,
                        a.PayAmount as PayAmount
                        from POSpay a
                        inner join Initiative b on  a.IdInitiative = b.Id
                        inner join Project c on a.IdProject = c.Id) a
                        left join
                        (Select * from POSPay where IdPOSPaysAdjust is not null) b on a.IdPosPays = b.IdPOSPaysAdjust) a
                        group by IdInitiative, IdProject, AmountInvoiced) Rem
                        left join Initiative a on Rem.IdInitiative = a.Id
                        left join Project b on Rem.IdProject = b.Id
                        ) rem2
                        left join 
                        (
                        select IdInitiative,IdProject, string_agg(concat(DescriptionPOS, ':', NumberTransfer), ', ') as Notes
                        from POSPay 
                        group by IdInitiative, IdProject
                        ) b
                        on rem2.IdInitiative = b.IdInitiative and rem2.IdProject = b.IdProject


                        Select rem2.InitiativeName, rem2.NameProject, rem2.AmountInvoiced, rem2.POsReceived, rem2.Balance, rem2.Adjustment, rem2.FinalBalance,  isnull(b.Notes,' ') as Notes
                        from 
                        (
                        Select	a.Id as IdInitiative,
		                        a.Name as InitiativeName, 
		                        b.Id as IdProject,
		                        b.Name as NameProject, 
		                        isnull(rem.AmountInvoiced,0) as AmountInvoiced, 
		                        isnull(rem.POsReceived,0) as POsReceived, 
		                        ((isnull(rem.AmountInvoiced,0) - isnull(rem.POsReceived,0))*-1) as Balance, 
		                        isnull(rem.Adjustment,0) as Adjustment, 
		                        ((isnull(rem.AmountInvoiced,0) - isnull(rem.POsReceived,0) + isnull(rem.Adjustment,0))*-1) as FinalBalance 
                        from 
                        (Select IdInitiative, IdProject, AmountInvoiced, sum(PayAmount) as POsReceived, sum(Adjustment) as Adjustment   from 
                        (Select a.IdInitiative, a.IdProject, a.IdPOSPays, a.AmountInvoiced, a.PayAmount, b.PayAmount as Adjustment from
                        (Select 
                        c.IdInitiative,
                        c.Id as idProject,
                        a.Id as IdPosPays,
                        c.AmountDefined as AmountInvoiced,
                        a.PayAmount as PayAmount
                        from POSpay a
                        inner join Initiative b on  a.IdInitiative = b.Id
                        inner join Project c on a.IdProject = c.Id) a
                        left join
                        (Select * from POSPay where IdPOSPaysAdjust is not null) b on a.IdPosPays = b.IdPOSPaysAdjust) a
                        group by IdInitiative, IdProject, AmountInvoiced) Rem
                        left join Initiative a on Rem.IdInitiative = a.Id
                        left join Project b on Rem.IdProject = b.Id
                        ) rem2
                        left join 
                        (
                        select IdInitiative,IdProject, string_agg(concat(DescriptionPOS, ':', NumberTransfer), ', ') as Notes
                        from POSPay 
                        group by IdInitiative, IdProject
                        ) b
                        on rem2.IdInitiative = b.IdInitiative and rem2.IdProject = b.IdProject
                        ORDER BY rem2.IdInitiative, rem2.IdProject
                        OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = _dbConnection.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<BudgetDTO> allTodos = reader.Read<BudgetDTO>().ToList();


            var result = new PagingResponseModel<List<BudgetDTO>>(allTodos, count, currentPageNumber, pageSize);
            return result;
        }


    }
}
