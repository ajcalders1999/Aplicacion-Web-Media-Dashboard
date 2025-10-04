using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PeliculasWeb.Models;
using System.Net.Http;

namespace PeliculasWeb.Controllers
{
    public class SeriesController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor: inicializa HttpClient con la URL base de la API
        public SeriesController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7244/api/") // Asegúrate que el puerto sea correcto
            };
        }

        // Acción principal para mostrar series filtradas por género
        public async Task<IActionResult> Index(int? idGenero)
        {
            // 1. Obtener lista de géneros de series desde la API
            var generoResponse = await _httpClient.GetAsync("Genero/Series");
            var jsonGeneros = await generoResponse.Content.ReadAsStringAsync();

            // Deserializar JSON a lista de géneros; si es null, crear lista vacía
            var generos = JsonConvert.DeserializeObject<List<GeneroViewModel>>(jsonGeneros) ?? new();

            // 2. Determinar el género seleccionado:
            //    - Si idGenero fue proporcionado y es válido, usarlo
            //    - Si no, usar el primer género disponible
            var primerGenero = generos.FirstOrDefault();
            var generoSeleccionado = (idGenero.HasValue && generos.Any(g => g.Id == idGenero.Value))
                ? idGenero.Value
                : primerGenero?.Id ?? 0;

            // 3. Obtener series para el género seleccionado desde la API
            var seriesResponse = await _httpClient.GetAsync($"Media/Series/{generoSeleccionado}");
            var jsonSeries = await seriesResponse.Content.ReadAsStringAsync();

            // Deserializar JSON a lista de series; si es null, crear lista vacía
            var series = JsonConvert.DeserializeObject<List<MediaViewModel>>(jsonSeries) ?? new();

            // 4. Obtener favoritos del usuario desde la API
            int usuarioId = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            var favoritosResponse = await _httpClient.GetAsync($"Favoritos/usuario/{usuarioId}");
            var jsonFavoritos = await favoritosResponse.Content.ReadAsStringAsync();

            // Deserializar JSON a lista de favoritos; si es null, crear lista vacía
            var favoritos = JsonConvert.DeserializeObject<List<FavoritoViewModel>>(jsonFavoritos) ?? new();

            // 5. Filtrar favoritos para incluir solo los de tipo "series"
            var favoritosIds = favoritos
                .Where(f => f.Tipo.ToLower() == "series")
                .Select(f => f.ItemId)
                .ToList();

            // 6. Construir el modelo para la vista con géneros, series, género seleccionado y favoritos
            var modelo = new HomeViewModel
            {
                Generos = generos,
                Peliculas = series,
                GeneroSeleccionado = generoSeleccionado,
                FavoritosIds = favoritosIds
            };

            // 7. Retornar la vista con el modelo completo
            return View(modelo);
        }
    }
}
