using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using PeliculasWeb.Models;
using PeliculasWeb.Services;

namespace PeliculasWeb.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly PeliculasService _peliculasService;

        // Constructor: inyecta el servicio de películas y un HttpClient configurado
        public PeliculasController(PeliculasService peliculasService, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7244"); // Cambia si tu API usa otro puerto
            _peliculasService = peliculasService;
        }

        // Acción principal: muestra lista de películas por género
        public async Task<IActionResult> Index(int? idGenero)
        {
            // 1. Obtener lista de géneros desde la API
            var generosResponse = await _httpClient.GetAsync("/api/genero/peliculas");
            var jsonGeneros = await generosResponse.Content.ReadAsStringAsync();
            var generos = JsonSerializer.Deserialize<List<GeneroViewModel>>(jsonGeneros, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Si no se pasa un género, usar el primero como predeterminado
            var id = idGenero ?? generos.First().Id;

            // 2. Obtener películas filtradas por género
            var peliculasResponse = await _httpClient.GetAsync($"/api/media/peliculas/{id}");
            var jsonPeliculas = await peliculasResponse.Content.ReadAsStringAsync();
            var peliculas = JsonSerializer.Deserialize<List<MediaViewModel>>(jsonPeliculas, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // 3. Variables para favoritos y watchlist del usuario
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var favoritosIds = new List<int>();
            var watchlistIds = new List<int>();

            // 4. Si hay usuario en sesión, obtener sus favoritos y watchlist
            if (usuarioId != null)
            {
                // Favoritos
                var favResponse = await _httpClient.GetAsync($"/api/favoritos/usuario/{usuarioId}");
                if (favResponse.IsSuccessStatusCode)
                {
                    var favJson = await favResponse.Content.ReadAsStringAsync();
                    var favoritos = JsonSerializer.Deserialize<List<FavoritoViewModel>>(favJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    favoritosIds = favoritos.Select(f => f.ItemId).ToList();
                }

                // Watchlist
                var watchlistResponse = await _httpClient.GetAsync($"/api/watchlist/usuario/{usuarioId}");
                if (watchlistResponse.IsSuccessStatusCode)
                {
                    var watchlistJson = await watchlistResponse.Content.ReadAsStringAsync();
                    var watchlist = JsonSerializer.Deserialize<List<WatchlistViewModel>>(watchlistJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    watchlistIds = watchlist
                        .Where(w => w.Tipo == "movie")
                        .Select(w => w.ItemId)
                        .ToList();
                }
            }

            // 5. Construir el modelo de datos para la vista
            var modelo = new HomeViewModel
            {
                Generos = generos,
                Peliculas = peliculas,
                GeneroSeleccionado = id,
                FavoritosIds = favoritosIds,
                WatchlistIds = watchlistIds
            };

            return View(modelo);
        }

        // Acción para mostrar detalles de una película específica
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"/api/peliculas/{id}");

            // Si la API no devuelve éxito, redirigir a la lista
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            // Deserializar el objeto película
            var json = await response.Content.ReadAsStringAsync();
            var pelicula = JsonSerializer.Deserialize<MediaViewModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(pelicula);
        }
    }
}
