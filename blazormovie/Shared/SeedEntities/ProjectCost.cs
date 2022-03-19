using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class ProjectCost
    {
        public int Id { get; set; }   
        public int ProjectId { get; set; }   
        public int CostId { get; set; }   
        public DateTime DateOfCost { get; set; }   
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public double Amount { get; set; }
    }
}
