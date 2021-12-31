﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class Initiative
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        List<Project> Projects { get; set; }

    }
}