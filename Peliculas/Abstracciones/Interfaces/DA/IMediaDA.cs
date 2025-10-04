
using Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces.DA
{
    public interface IMediaDA
    {
        Task<IEnumerable<Media>> ObtenerPeliculasPorGenero(int generoId);
        Task<IEnumerable<Media>> ObtenerSeriesPorGenero(int generoId);
    }
}
