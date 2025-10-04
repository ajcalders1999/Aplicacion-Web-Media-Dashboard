
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peliculas.Flujo
{
    public class GeneroFlujo : IGeneroFlujo
    {
        private readonly IMovieDBServicio _movieDBServicio;

        public GeneroFlujo(IMovieDBServicio movieDBServicio)
        {
            _movieDBServicio = movieDBServicio;
        }

        public async Task<IEnumerable<Genero>> ObtenerGeneros()
        {
            return await _movieDBServicio.ObtenerGenerosDesdeAPI();
        }
        public async Task<IEnumerable<Genero>> ObtenerGenerosPeliculas()
        {
            return await _movieDBServicio.ObtenerGenerosPeliculasDesdeAPI();
        }

        public async Task<IEnumerable<Genero>> ObtenerGenerosSeries()
        {
            return await _movieDBServicio.ObtenerGenerosSeriesDesdeAPI();
        }

    }
}
