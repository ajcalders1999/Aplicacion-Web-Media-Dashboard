using PeliculasWeb.Models;

namespace PeliculasWeb.Services
{
    public class VisualizacionService : ApiService
    {
        // Obtiene la lista completa de visualizaciones desde la API
        public async Task<List<VisualizacionViewModel>> ObtenerListaVisualizacion()
        {
            return await GetAsync<List<VisualizacionViewModel>>("visualizaciones");
        }

        // Agrega una nueva visualización a la lista enviándola a la API
        public async Task<bool> AgregarALista(VisualizacionViewModel visualizacion)
        {
            return await PostAsync("visualizaciones", visualizacion);
        }

        // Elimina una visualización de la lista mediante su Id
        public async Task<bool> EliminarDeLista(int id)
        {
            return await DeleteAsync($"visualizaciones/{id}");
        }
    }
}
