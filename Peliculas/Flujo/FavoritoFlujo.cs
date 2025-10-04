
using Abstracciones.Modelos;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;

namespace Peliculas.Flujo
{
    public class FavoritoFlujo : IFavoritoFlujo
    {
        private readonly IFavoritoRepositorio _repositorio;
        private readonly IMovieDBServicio _movieDBServicio;

        public FavoritoFlujo(IFavoritoRepositorio repositorio, IMovieDBServicio movieDBServicio)
        {
            _repositorio = repositorio;
            _movieDBServicio = movieDBServicio;
        }
        public async Task<IEnumerable<FavoritoConDetalles>> ObtenerFavoritosConDetalles(int usuarioId)
        {
            var favoritos = (await ObtenerFavoritos(usuarioId)).ToList();
            var resultado = new List<FavoritoConDetalles>();

            foreach (var fav in favoritos)
            {
                var detalle = new FavoritoConDetalles
                {
                    Id = fav.Id,
                    UsuarioId = fav.UsuarioId,
                    ItemId = fav.ItemId,
                    Tipo = fav.Tipo,
                    Comentario = fav.Comentario,
                    Calificacion = fav.Calificacion
                };

                var media = fav.Tipo.ToLower() == "movie"
                    ? await _movieDBServicio.ObtenerPeliculaPorId(fav.ItemId)
                    : await _movieDBServicio.ObtenerSeriePorId(fav.ItemId);

                if (media != null)
                {
                    detalle.Titulo = media.Title;
                    detalle.ImageURL = media.ImageURL;
                    detalle.Description = media.Description;
                    detalle.ReleaseDate = media.ReleaseDate;
                    detalle.Rating = media.Rating;
                }

                resultado.Add(detalle);
            }

            return resultado;
        }

        public async Task<IEnumerable<Favorito>> ObtenerFavoritos(int usuarioId)
        {
            return await _repositorio.ObtenerFavoritos(usuarioId);
        }

        public async Task AgregarFavorito(Favorito favorito)
        {
            await _repositorio.AgregarFavorito(favorito);
        }

        public async Task EliminarFavorito(int id)
        {
            await _repositorio.EliminarFavorito(id);
        }

        public async Task<bool> ActualizarFavorito(ActualizarFavoritoDto favorito)
        {
            return await _repositorio.ActualizarFavorito(favorito);
        }
    }
}
