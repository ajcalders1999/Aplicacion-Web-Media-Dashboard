using PeliculasWeb.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PeliculasWeb.Services
{
    public class WatchlistService
    {
        private readonly HttpClient _httpClient;

        // Constructor que recibe HttpClient y configura la base URL de la API
        public WatchlistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7244/api/"); // Cambiar si tu API usa otro puerto
        }

        // Obtiene la lista de watchlist para un usuario específico
        public async Task<List<WatchlistViewModel>> ObtenerWatchlistAsync(int usuarioId)
        {
            var response = await _httpClient.GetAsync($"/api/watchlist/usuario/{usuarioId}");
            response.EnsureSuccessStatusCode(); // Lanza excepción si falla
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<WatchlistViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Obtiene un item específico del watchlist por su Id
        public async Task<WatchlistViewModel> ObtenerPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"watchlist/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WatchlistViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Crea un nuevo item en el watchlist enviando datos a la API
        public async Task CrearAsync(WatchlistViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("watchlist", content);
            response.EnsureSuccessStatusCode();
        }

        // Edita un item existente del watchlist enviando datos actualizados a la API
        public async Task EditarAsync(WatchlistViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"watchlist/{model.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        // Elimina un item del watchlist por su Id
        public async Task EliminarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"watchlist/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
