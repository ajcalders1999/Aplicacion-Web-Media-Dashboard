
using Abstracciones.Modelos;
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces.Flujo
{
    public interface IFavoritoFlujo
    {
        Task<IEnumerable<Favorito>> ObtenerFavoritos(int usuarioId);
        Task<bool> ActualizarFavorito(ActualizarFavoritoDto favorito);
        Task AgregarFavorito(Favorito favorito);
        Task EliminarFavorito(int id);
        Task<IEnumerable<FavoritoConDetalles>> ObtenerFavoritosConDetalles(int usuarioId);
    }
}
