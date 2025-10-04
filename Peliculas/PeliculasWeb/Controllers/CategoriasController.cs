using Microsoft.AspNetCore.Mvc;
using Peliculas.Servicios;
using PeliculasWeb.Services;

namespace PeliculasWeb.Controllers
{
    public class CategoriasController : Controller // Controlador para manejar categorías de películas y series
    {
        private readonly MovieDBServicio _movieService; // Servicio para obtener datos desde la API de películas

        // Constructor que recibe el servicio por inyección de dependencias
        public CategoriasController(MovieDBServicio movieService)
        {
            _movieService = movieService;
        }

        // Acción para obtener y mostrar los géneros de películas
        public async Task<IActionResult> GenerosPeliculas()
        {
            var generos = await _movieService.ObtenerGenerosPeliculasDesdeAPI(); // Llama al servicio para obtener géneros
            return View("GenerosPeliculas", generos); // Retorna la vista con la lista de géneros
        }

        // Acción para obtener y mostrar los géneros de series
        public async Task<IActionResult> GenerosSeries()
        {
            var generos = await _movieService.ObtenerGenerosSeriesDesdeAPI(); // Llama al servicio para obtener géneros
            return View("GenerosSeries", generos); // Retorna la vista con la lista de géneros
        }
    }
}
