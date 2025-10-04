using PeliculasWeb.Models;

namespace PeliculasWeb.Services
{
    public class SeriesService : ApiService
    {
        // Método para obtener una lista de series filtradas por género
        public async Task<List<SerieViewModel>> ObtenerSeriesPorGenero(int idGenero)
        {
            // Llama al método GetAsync heredado para obtener datos desde el endpoint específico
            return await GetAsync<List<SerieViewModel>>($"media/series/{idGenero}");
        }
    }
}
