
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IFavoritosDA
    {
        Task AgregarFavorito(Favorito favorito);
        Task<IEnumerable<Favorito>> ObtenerFavoritosPorUsuario(int userId);
        Task EliminarFavorito(int favoritoId);
    }
}
