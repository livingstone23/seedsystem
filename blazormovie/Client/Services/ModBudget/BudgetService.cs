using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class BudgetService : IBudgetService
    {
        private readonly HttpClient _httpClient;

        public BudgetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<Budget>> GetBudgets()
        {
            throw new System.NotImplementedException();
        }
    }
}
//fijar en initiative