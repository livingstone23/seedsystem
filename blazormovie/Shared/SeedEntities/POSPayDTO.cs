using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class POSPayDTO
    {
        public int Id { get; set; }

        public DateTime PayDay { get; set; }

        public string CurrencyPay { get; set; }

        public string DescriptionPOS { get; set; }

        public string NumberTransfer { get; set; }

        public decimal PayAmount { get; set; }

        public decimal RateChange { get; set; }

        public int IdProject { get; set; }

        public int IdInitiative { get; set; }

        public int? IdPOSPaysAdjust { get; set; }

        public string InitiativeName { get; set; }

        public string ProjectName { get; set; }
    }
}
