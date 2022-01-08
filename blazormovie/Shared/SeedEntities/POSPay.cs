using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class POSPay
    {
        [Key]
        public int Id { get; set; }


        [Column("PayDay")]
        //[Required(ErrorMessage = @" The {0} is a required field.  ")]
        public DateTime? PayDay { get; set; }

        [Column("CurrencyPay")]
        [MaxLength(10, ErrorMessage = @" The {0} must have a maximum length of {1}. ")]
        public string CurrencyPay { get; set; }

        [Column("DescriptionPOS")]
        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        [MaxLength(500, ErrorMessage = @" The {0} must have a maximum length of {1}. ")]
        public string DescriptionPOS { get; set; }

        [Column("NumberTransfer")]
        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        [MaxLength(100, ErrorMessage = @" The {0} must have a maximum length of {1}. ")]
        public string NumberTransfer { get; set; }

        [Column("PayAmount")]
        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        [Range(typeof(decimal), "1", "999999999", ErrorMessage = @" The {0} Must be in a valid range. ")]
        public decimal PayAmount { get; set; }

        [Column("RateChange")]
        public decimal RateChange { get; set; }

        [Column("IdProject")]
        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        public int IdProject { get; set; }

        [Column("IdInitiative")]
        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        public int IdInitiative { get; set; }

        public int? IdPOSPaysAdjust { get; set; }

    }
}
