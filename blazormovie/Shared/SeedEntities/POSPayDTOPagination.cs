using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class POSPayDTOPagination
    {
        public int TotalRecords { get; set; }
        public IEnumerable<POSPayDTO> Data { get; set; }

    }
}
