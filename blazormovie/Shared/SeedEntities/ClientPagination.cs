using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class ClientPagination
    {
        public int TotalRecords { get; set; }
        public IEnumerable<Client> Data { get; set; }

    }
}
