using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class BudgetDTO
    {
        public string InitiativeName { get; set; }

        public string NameProject { get; set; }

        public decimal AmountInvoiced { get; set; }

        public decimal POSreceived { get; set; }

        public decimal Balance { get; set; }

        public decimal Adjustment { get; set; }

        public decimal FinalBalance { get; set; }

        public string Notes { get; set; }
    }
}
