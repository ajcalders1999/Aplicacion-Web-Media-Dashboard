using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PeliculasWeb.Models;
using System.Net.Http;
using System.Text;

namespace PeliculasWeb.Controllers
{
    public class FavoritosController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor: inicializa HttpClient usando IHttpClientFactory
        public FavoritosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7244/api/");
        }

        // Muestra la lista de favoritos del usuario
        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return RedirectToAction("Login", "Cuenta"); // Redirige si no está logueado

            var response = await _httpClient.GetAsync($"favoritos/usuario/{usuarioId}");

            if (!response.IsSuccessStatusCode)
                return View(new List<FavoritoViewModel>()); // Si falla, retorna lista vacía

            var json = await response.Content.ReadAsStringAsync();
            var favoritos = JsonConvert.DeserializeObject<List<FavoritoViewModel>>(json);

            return View(favoritos);
        }

        // Muestra los detalles de un favorito específico
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"favoritos/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var json = await response.Content.ReadAsStringAsync();
            var favorito = JsonConvert.DeserializeObject<FavoritoViewModel>(json);
            return View(favorito);
        }

        // Edita un favorito existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FavoritoViewModel favorito)
        {
            var json = JsonConvert.SerializeObject(favorito);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("favoritos", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(favorito);
        }

        // Elimina un favorito por su ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"favoritos/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Mensaje"] = "✅ Favorito eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "❌ Error al eliminar el favorito.";
            }
            return RedirectToAction("Index");
        }

        // Agrega un favorito desde la vista de una película o serie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarDesdeVista(MediaViewModel media)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return RedirectToAction("Login", "Cuenta");

            var favorito = new FavoritoViewModel
            {
                UsuarioId = usuarioId.Value,
                ItemId = media.Id,
                Tipo = media.MediaType ?? "movie",
                Comentario = $"Agregado desde la vista de {(media.MediaType ?? "movie")}",
                Calificacion = (int)Math.Round(media.Rating),
                Title = media.Title,
                Description = media.Description,
                ImageURL = media.ImageURL,
                ReleaseDate = media.ReleaseDate
            };

            var json = JsonConvert.SerializeObject(favorito);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("favoritos", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "❌ Ocurrió un error al agregar a favoritos.";
            return RedirectToAction("Index", "Peliculas");
        }
    }
}
