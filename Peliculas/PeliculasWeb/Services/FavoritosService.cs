using PeliculasWeb.Models;
using System.Text.Json;
using System.Text;

namespace PeliculasWeb.Services
{
    public class FavoritosService
    {
        private readonly HttpClient _httpClient;

        // Constructor que recibe HttpClient e inicializa la base URL de la API
        public FavoritosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7244/api/"); // Cambia si tu API usa otro puerto
        }

        // Método para obtener la lista completa de favoritos
        public async Task<List<FavoritoViewModel>> ObtenerFavoritosAsync()
        {
            var response = await _httpClient.GetAsync("favoritos");
            response.EnsureSuccessStatusCode(); // Lanza excepción si no es éxito
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<FavoritoViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Método para obtener un favorito por su Id
        public async Task<FavoritoViewModel> ObtenerFavoritoPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"favoritos/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<FavoritoViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Método para crear un nuevo favorito enviando los datos en JSON
        public async Task CrearFavoritoAsync(FavoritoViewModel favorito)
        {
            var content = new StringContent(JsonSerializer.Serialize(favorito), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("favoritos", content);
            response.EnsureSuccessStatusCode();
        }

        // Método para actualizar un favorito existente mediante su Id
        public async Task EditarFavoritoAsync(FavoritoViewModel favorito)
        {
            var content = new StringContent(JsonSerializer.Serialize(favorito), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"favoritos/{favorito.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        // Método para eliminar un favorito por Id
        public async Task EliminarFavoritoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"favoritos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
