using Microsoft.EntityFrameworkCore;
using PeliculasWeb.Models;

namespace PeliculasWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor que recibe opciones (inyección de dependencias)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet para los actores
        public DbSet<Actor> Actores { get; set; }

        // DbSet para el registro de usuarios
        public DbSet<UsuarioModel> User { get; set; }

        // Si tienes otros modelos, agrégalos aquí:
        // public DbSet<Pelicula> Peliculas { get; set; }
        // public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones personalizadas si las necesitas
        }
    }
}
