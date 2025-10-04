
using Abstracciones.Modelos;
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IFavoritoRepositorio
    {
        Task<IEnumerable<Favorito>> ObtenerFavoritos(int usuarioId);
        Task AgregarFavorito(Favorito favorito);
        Task EliminarFavorito(int id);
        Task<bool> ActualizarFavorito(ActualizarFavoritoDto favorito);

    }
}
