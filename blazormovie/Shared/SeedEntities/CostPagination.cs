using System.Collections.Generic;

namespace blazormovie.Shared.SeedEntities
{
    public class CostPagination
    {
        public int TotalRecords { get; set; }
        public IEnumerable<Cost> Data { get; set; }
    }
}
