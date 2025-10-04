using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace PeliculasWeb.Services
{
    public class ApiService
    {
        // Cliente HTTP para hacer llamadas a la API
        private readonly HttpClient _httpClient;

        // Constructor que inicializa HttpClient con base URL y encabezado Accept JSON
        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7244/api/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Método genérico GET que obtiene datos desde un endpoint y los deserializa al tipo T
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return default;
        }

        // Método genérico POST para enviar datos serializados como JSON y devuelve éxito o fallo
        public async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }

        // Método genérico PUT para actualizar datos en un endpoint, enviando JSON, devuelve éxito o fallo
        public async Task<bool> PutAsync<T>(string endpoint, T data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }

        // Método para eliminar un recurso usando DELETE, devuelve éxito o fallo
        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
    }
}
