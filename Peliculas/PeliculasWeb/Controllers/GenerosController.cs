using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PeliculasWeb.Models; // Importa el modelo GeneroViewModel
using System.Net.Http;

namespace PeliculasWeb.Controllers
{
    public class GenerosController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor: Inicializa HttpClient con la dirección base de la API
        public GenerosController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7009/api/")
            };
        }

        // Acción que obtiene la lista de géneros de películas desde la API
        // GET: /Generos/Peliculas
        public async Task<IActionResult> Peliculas()
        {
            // Llama a la API para obtener géneros de películas
            var response = await _httpClient.GetAsync("Genero/Peliculas");

            if (response.IsSuccessStatusCode)
            {
                // Convierte el JSON en una lista de objetos GeneroViewModel
                var json = await response.Content.ReadAsStringAsync();
                var generos = JsonConvert.DeserializeObject<List<GeneroViewModel>>(json);

                return View(generos); // Retorna la vista con los datos
            }

            // Si hay error, retorna una lista vacía
            return View(new List<GeneroViewModel>());
        }

        // Acción que obtiene la lista de géneros de series desde la API
        // GET: /Generos/Series
        public async Task<IActionResult> Series()
        {
            // Llama a la API para obtener géneros de series
            var response = await _httpClient.GetAsync("Genero/Series");

            if (response.IsSuccessStatusCode)
            {
                // Convierte el JSON en una lista de objetos GeneroViewModel
                var json = await response.Content.ReadAsStringAsync();
                var generos = JsonConvert.DeserializeObject<List<GeneroViewModel>>(json);

                return View(generos); // Retorna la vista con los datos
            }

            // Si hay error, retorna una lista vacía
            return View(new List<GeneroViewModel>());
        }
    }
}
