using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IWatchlistDA
    {
        Task AgregarAWatchlist(Watchlist item);
        Task<IEnumerable<Watchlist>> ObtenerWatchlistPorUsuario(int userId);
        Task EliminarDeWatchlist(int watchlistId);
    }
}
