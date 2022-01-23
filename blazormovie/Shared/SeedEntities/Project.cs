using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blazormovie.Shared.SeedEntities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        [MaxLength(30, ErrorMessage = @" The {0} must have a maximum length of {1}. ")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = @" The {0} must have a maximum length of {1}. ")]
        [Column("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        [Column("AmountDefined")]
        [Range(typeof(decimal),"1","999999999",ErrorMessage = @" The {0} Must be in a valid range. ")]
        public decimal AmountDefined { get; set; }

        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        [Column("IdInitiative")]
        public int IdInitiative { get; set; }

        [NotMapped]
        public int TotalPays { get; set; }
        [NotMapped]
        public string InitiativeName { get; set; }
        public List<POSPay> POSPays { get; set; }

    }

}
