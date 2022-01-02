using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class Initiative
    {

        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public int TotalProjects { get; set; }
        public List<Project> Projects { get; set; }

    }
}
