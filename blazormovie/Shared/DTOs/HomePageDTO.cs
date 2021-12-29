using System;
using System.Collections.Generic;
using blazormovie.Shared.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.DTOs
{
    public class HomePageDTO
    {
        public List<Pelicula> PeliculasEnCartelera { get; set; }
        public List<Pelicula> ProximosEstrenos { get; set; }
    }
}
