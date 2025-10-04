
using Peliculas.Abstractions.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces.Flujo
{
    public interface IMediaFlujo
    {
        Task<IEnumerable<Media>> ObtenerPeliculasPorGenero(int generoId);
        Task<IEnumerable<Media>> ObtenerSeriesPorGenero(int generoId);
    }
}
