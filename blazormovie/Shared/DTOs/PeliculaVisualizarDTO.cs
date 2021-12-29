using blazormovie.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.DTOs
{
    public class PeliculaVisualizarDTO
    {
        public Pelicula Pelicula { get; set; }
        public List<Genero> Generos { get; set; }
        public List<Persona> Actores { get; set; }
        public int VotoUsuario { get; set; }
        public double PromedioVotos { get; set; }
    }
}
