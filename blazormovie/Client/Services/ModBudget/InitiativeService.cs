﻿using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class InitiativeService : IInitiativeService
    {
        public Task<IEnumerable<Budget>> GetInitiatives()
        {
            throw new System.NotImplementedException();
        }
    }
}
