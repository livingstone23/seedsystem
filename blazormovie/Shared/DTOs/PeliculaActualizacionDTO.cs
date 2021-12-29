using blazormovie.Shared.Entities;
using System.Collections.Generic;

namespace blazormovie.Shared.DTOs
{
    public class PeliculaActualizacionDTO
    {
        public Pelicula Pelicula { get; set; }
        public List<Persona> Actores { get; set; }
        public List<Genero> GenerosSeleccionados { get; set; }
        public List<Genero> GenerosNoSeleccionados { get; set; }
    }
}
