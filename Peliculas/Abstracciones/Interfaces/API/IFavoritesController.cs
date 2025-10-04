
using Microsoft.AspNetCore.Mvc;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IFavoritesController
    {
        Task<IActionResult> AgregarFavorito([FromBody] Favorito favorito);
        Task<IActionResult> ObtenerFavoritosPorUsuario([FromRoute] int userId);
        Task<IActionResult> EliminarFavorito([FromRoute] int favoritoId);
    }
}
