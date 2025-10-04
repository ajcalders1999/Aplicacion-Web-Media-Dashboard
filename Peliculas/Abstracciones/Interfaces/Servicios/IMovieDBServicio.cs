using Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IMovieDBServicio
    {
        public Task<IEnumerable<Media>> ObtenerPeliculasPorGenero(int generoId);
        public Task<IEnumerable<Media>> ObtenerSeriesPorGenero(int generoId);
        public Task<IEnumerable<Genero>> ObtenerGenerosDesdeAPI();
        public Task<IEnumerable<Genero>> ObtenerGenerosPeliculasDesdeAPI();
        public Task<IEnumerable<Genero>> ObtenerGenerosSeriesDesdeAPI();
        Task<Media> ObtenerPeliculaPorId(int id);
        Task<Media> ObtenerSeriePorId(int id);


    }
}

