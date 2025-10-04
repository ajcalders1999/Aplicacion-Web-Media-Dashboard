using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PeliculasWeb.Models;
using PeliculasWeb.Services;
using System.Net.Http;

namespace PeliculasWeb.Controllers
{
    public class WatchlistController : Controller
    {
        private readonly WatchlistService _watchlistService;
        private readonly HttpClient _httpClient;

        // Constructor: inicializa el servicio y el cliente HTTP con base de API
        public WatchlistController(WatchlistService watchlistService, IHttpClientFactory httpClientFactory)
        {
            _watchlistService = watchlistService;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7244/api/");
        }

        // GET: /Watchlist - Muestra la lista de watchlist para el usuario
        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
                return RedirectToAction("Login", "Cuenta");

            var response = await _httpClient.GetAsync($"watchlist/usuario/{usuarioId}/detalles");

            if (!response.IsSuccessStatusCode)
                return View(new List<WatchlistViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<WatchlistViewModel>>(json);

            return View(lista);
        }

        // GET: /Watchlist/Details/5 - Muestra detalles de un ítem específico de la watchlist
        public async Task<IActionResult> Details(int id)
        {
            var item = await _watchlistService.ObtenerPorIdAsync(id);
            return View(item);
        }

        // GET: /Watchlist/Create - Muestra formulario para crear un nuevo ítem en watchlist
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Watchlist/Create - Procesa el formulario para crear un nuevo ítem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WatchlistViewModel model)
        {
            int? usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToAction("Login", "Cuenta");

            model.UsuarioId = usuarioId.Value;

            await _watchlistService.CrearAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // POST: Edita la prioridad de un ítem en la watchlist usando JSON en el cuerpo
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] WatchlistViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync("watchlist/prioridad", model);
            if (!response.IsSuccessStatusCode)
                return BadRequest();

            return Ok();
        }

        // POST: /Watchlist/Delete/5 - Elimina un ítem de la watchlist por id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"watchlist/{id}");
            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index"); // podrías mostrar error si deseas

            return RedirectToAction(nameof(Index));
        }
    }
}
