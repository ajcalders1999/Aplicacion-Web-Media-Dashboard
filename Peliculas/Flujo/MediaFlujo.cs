using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos;

namespace Peliculas.Flujo
{
    public class MediaFlujo : IMediaFlujo
    {
        private readonly IMovieDBServicio _movieDBServicio;

        public MediaFlujo(IMovieDBServicio movieDBServicio)
        {
            _movieDBServicio = movieDBServicio;
        }

        public async Task<IEnumerable<Media>> ObtenerPeliculasPorGenero(int generoId)
        {
            return await _movieDBServicio.ObtenerPeliculasPorGenero(generoId);
        }

        public async Task<IEnumerable<Media>> ObtenerSeriesPorGenero(int generoId)
        {
            return await _movieDBServicio.ObtenerSeriesPorGenero(generoId);
        }
        public async Task<List<Media>> ObtenerTodasLasPeliculas()
        {
            return null;//return await _context.Peliculas.ToListAsync();
        }
    }
}
