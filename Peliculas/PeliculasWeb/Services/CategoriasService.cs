using PeliculasWeb.Models;

namespace PeliculasWeb.Services
{
    // Servicio que hereda ApiService para gestionar categorías específicas
    public class CategoriasService : ApiService
    {
        // Obtiene la lista de géneros de películas desde el endpoint correspondiente
        public async Task<List<CategoriaViewModel>> ObtenerGenerosPeliculas()
        {
            return await GetAsync<List<CategoriaViewModel>>("categorias/peliculas");
        }

        // Obtiene la lista de géneros de series desde el endpoint correspondiente
        public async Task<List<CategoriaViewModel>> ObtenerGenerosSeries()
        {
            return await GetAsync<List<CategoriaViewModel>>("categorias/series");
        }
    }
}
