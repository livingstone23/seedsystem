using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class InitiativePagination
    {
        public int TotalRecords { get; set; }
        public IEnumerable<Initiative> Data { get; set; }
    }
}
