
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IWatchlistRepositorio
    {
        Task<IEnumerable<Watchlist>> ObtenerWatchlist(int usuarioId);
        Task AgregarWatchlist(Watchlist watchlist);
        Task EliminarWatchlist(int id);
        Task<Watchlist> ObtenerWatchlistPorId(int id);
        Task<bool> ActualizarWatchlist(Watchlist watchlist);
        Task<IEnumerable<Watchlist>> ObtenerTodosAsync();
        Task<Watchlist> ObtenerPorId(int id);

    }
}
