using Microsoft.AspNetCore.Mvc;
using PeliculasWeb.Models;
using System.Net.Http;
using System.Text.Json;

namespace PeliculasWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor: recibe HttpClient (inyectado por DI) y configura la URL base de la API
        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7244"); // Cambiar si el puerto del backend es distinto
        }

        // Acción principal que carga la página de inicio
        public async Task<IActionResult> Index(int? generoId)
        {
            // 1. Llamada a la API para obtener todos los géneros de películas
            var generosResponse = await _httpClient.GetAsync("/api/genero/peliculas");
            var jsonGeneros = await generosResponse.Content.ReadAsStringAsync();

            // Deserializa el JSON a una lista de géneros
            var generos = JsonSerializer.Deserialize<List<GeneroViewModel>>(jsonGeneros, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignora mayúsculas/minúsculas en las propiedades
            });

            // 2. Si no se selecciona un género, usar el primero por defecto
            var id = generoId ?? generos.First().Id;

            // 3. Llamada a la API para obtener las películas del género seleccionado
            var peliculasResponse = await _httpClient.GetAsync($"/api/media/peliculas/{id}");
            var jsonPeliculas = await peliculasResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Usa nombres en camelCase
                PropertyNameCaseInsensitive = true
            };

            // Deserializa las películas obtenidas
            var peliculas = JsonSerializer.Deserialize<List<MediaViewModel>>(jsonPeliculas, options);

            // Debug en consola (solo para desarrollo)
            Console.WriteLine($"📦 JSON Películas: {jsonPeliculas}");
            Console.WriteLine($"🎬 Películas deserializadas: {peliculas?.Count}");

            // 4. Crear un modelo para enviar a la vista
            var modelo = new HomeViewModel
            {
                Generos = generos,
                Peliculas = peliculas,
                GeneroSeleccionado = id
            };

            // Retorna la vista junto con el modelo de datos
            return View(modelo);
        }
    }
}
