using Microsoft.AspNetCore.Mvc;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IWatchlistController
    {
        Task<IActionResult> AgregarAWatchlist([FromBody] Watchlist item);
        Task<IActionResult> ObtenerWatchlistPorUsuario([FromRoute] int userId);
        Task<IActionResult> EliminarDeWatchlist([FromRoute] int watchlistId);
    }
}
