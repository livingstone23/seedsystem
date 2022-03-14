using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.Entities
{
    public class Exchanges
    {
        public int Id{ get; set; }
        public int IdPo { get; set; }
        public double Pounds { get; set; }
        [Column("Exchange")]
        public double Exchange { get; set; }
    }
}
