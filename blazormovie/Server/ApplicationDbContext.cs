using blazormovie.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blazormovie.Server
{
    public class ApplicationDbContext : IdentityDbContext    //DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }


        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<GeneroPelicula>(builder =>
            //    {
            //        builder.HasNoKey();
            //        //builder.ToTable("GeneroPelicula");
            //    });
            modelBuilder.Entity<GeneroPelicula>().HasNoKey();
            modelBuilder.Entity<GeneroPelicula>().HasKey(x => new { x.GeneroId, x.PeliculaId });
            modelBuilder.Entity<PeliculaActor>().HasKey(x => new { x.PeliculaId, x.PersonaId });


            var roleAdmin = new IdentityRole()
            { Id = "ee9d2e80-d375-4d0c-a97a-0d5bd044b81f", Name= "admin", NormalizedName ="admin" };

            modelBuilder.Entity<IdentityRole>().HasData(roleAdmin);

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<GeneroPelicula> GenerosPeliculas { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<PeliculaActor> PeliculasActores { get; set; }



    }
}
