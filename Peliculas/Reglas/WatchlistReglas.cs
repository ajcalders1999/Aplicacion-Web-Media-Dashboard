using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reglas
{
    public class WatchlistReglas : IWatchlistReglas
    {
        private readonly IWatchlistFlujo _watchlistFlujo;

        // Constructor que recibe la implementación del flujo para watchlist
        public WatchlistReglas(IWatchlistFlujo watchlistFlujo)
        {
            _watchlistFlujo = watchlistFlujo;
        }

        // Valida que no se agregue un item repetido a la watchlist y luego lo agrega
        public async Task ValidarAgregarAWachlist(Watchlist item)
        {
            // Obtiene la lista de watchlist del usuario
            var watchlist = await _watchlistFlujo.ObtenerWatchlistPorUsuario(item.UsuarioId);

            // Si ya existe un item con el mismo Id, lanza una excepción
            if (watchlist.Any(w => w.Id == item.ItemId))
            {
                throw new InvalidOperationException("Esta película o serie ya está en la lista de visualización.");
            }

            // Si no está repetido, agrega el item a la lista
            await _watchlistFlujo.AgregarWatchlist(item);
        }
    }
}
