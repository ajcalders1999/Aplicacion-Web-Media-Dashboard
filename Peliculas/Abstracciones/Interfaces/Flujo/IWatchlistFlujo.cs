
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces.Flujo
{
    public interface IWatchlistFlujo
    {
        Task AgregarWatchlist(Watchlist watchlist);
        Task EliminarWatchlist(int id);
        Task<List<Watchlist>> ObtenerWatchlistPorUsuario(int usuarioId);
        Task<IEnumerable<Watchlist>> ObtenerWatchlist(int usuarioId);
        Task<bool> ActualizarWatchlist(Watchlist watchlist);
        Task<Watchlist> ObtenerWatchlistPorId(int id);

    }
}
