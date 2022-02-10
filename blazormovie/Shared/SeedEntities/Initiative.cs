using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class Initiative
    {
        [Key]
        public int Id { get; set; }

        [Column("Name")]
        [Required(ErrorMessage = @" The {0} is a required field.  ")]
        [MaxLength(100, ErrorMessage = @" The {0} must have a maximum length of {1}. ")]
        public string Name { get; set; }

        [Column("Description")]
        [MaxLength(500, ErrorMessage = @" The {0} must have a maximum length of {1}. ")]
        public string Description { get; set; }

        [NotMapped]
        public int TotalProjects { get; set; }
        public List<Project> Projects { get; set; }

    }
}
