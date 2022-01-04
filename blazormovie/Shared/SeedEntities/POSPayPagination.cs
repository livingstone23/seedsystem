using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class POSPayPagination
    {
        public int TotalRecords { get; set; }
        public IEnumerable<POSPay> Data { get; set; }
    }
}
