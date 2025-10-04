using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reglas
{
    internal class FavoritoReglas
    {
        private readonly IFavoritoFlujo _favoritoFlujo;

        // Constructor que recibe la implementación del flujo de favoritos
        public FavoritoReglas(IFavoritoFlujo favoritoFlujo)
        {
            _favoritoFlujo = favoritoFlujo;
        }

        // Método para validar antes de agregar un favorito
        public async Task ValidarAgregarFavorito(Favorito favorito)
        {
            // Obtiene la lista de favoritos del usuario
            var favoritos = await _favoritoFlujo.ObtenerFavoritos(favorito.UsuarioId);

            // Verifica si el ítem ya existe en favoritos y lanza excepción si es así
            if (favoritos.Any(f => f.ItemId == favorito.ItemId))
            {
                throw new InvalidOperationException("Esta película o serie ya está en favoritos.");
            }
        }
    }
}
