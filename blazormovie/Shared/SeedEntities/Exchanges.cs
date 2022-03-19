using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class Exchanges
    {
        public int Id{ get; set; }
        public int IdPo { get; set; }
        public decimal Pounds { get; set; }
        [Column("Exchange")]
        public decimal Exchange { get; set; }
    }
}
