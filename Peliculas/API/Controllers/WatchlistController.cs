
using Microsoft.AspNetCore.Mvc;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Modelos;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using Abstracciones.Modelos;
using Newtonsoft.Json;

namespace Peliculas.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistFlujo _flujo;

        public WatchlistController(IWatchlistFlujo flujo)
        {
            _flujo = flujo;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarWatchlist([FromBody] Watchlist watchlist)
        {
            if (watchlist == null)
                return BadRequest("El elemento de la watchlist es requerido.");

            await _flujo.AgregarWatchlist(watchlist);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarWatchlist(int id)
        {
            await _flujo.EliminarWatchlist(id);
            return NoContent();
        }
        // 🆕 GET por ID (detalle)
        [HttpGet("detalle/{id}")]
        public async Task<IActionResult> ObtenerWatchlistPorId(int id)
        {
            try
            {
                var item = await _flujo.ObtenerWatchlist(id);
                if (item == null)
                    return NotFound();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener item de la watchlist: {ex.Message}");
            }
        }


        // 🆕 PUT (editar)
        // PUT: api/watchlist/prioridad
        [HttpPut("prioridad")]
        public async Task<IActionResult> ActualizarPrioridad([FromBody] ActualizarPrioridadDto dto)
        {
            try
            {
                var item = await _flujo.ObtenerWatchlistPorId(dto.Id);
                if (item == null)
                    return NotFound();

                item.Prioridad = dto.Prioridad;

                var actualizado = await _flujo.ActualizarWatchlist(item);
                if (!actualizado)
                    return BadRequest("No se pudo actualizar la prioridad.");

                return Ok("Prioridad actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error del servidor: {ex.Message}");
            }
        }


        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObtenerWatchlist(int usuarioId)
        {
            var watchlist = await _flujo.ObtenerWatchlistPorUsuario(usuarioId);
            return Ok(watchlist);
        }
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerPorUsuario(int usuarioId)
        {
            var resultado = await _flujo.ObtenerWatchlist(usuarioId);
            return Ok(resultado);
        }
        // GET: api/watchlist/usuario/{usuarioId}/detalles
        [HttpGet("usuario/{usuarioId}/detalles")]
        public async Task<IActionResult> ObtenerWatchlistConDetalles(int usuarioId)
        {
            try
            {
                var lista = await _flujo.ObtenerWatchlistPorUsuario(usuarioId);

                var resultado = new List<WatchlistConDetallesDto>();

                foreach (var item in lista)
                {
                    string url = item.Tipo.ToLower() == "movie"
                        ? $"https://api.themoviedb.org/3/movie/{item.ItemId}?api_key=d49e442f7fd045de13717ddfd651331d&language=es-ES"
                        : $"https://api.themoviedb.org/3/tv/{item.ItemId}?api_key=d49e442f7fd045de13717ddfd651331d&language=es-ES";

                    var response = await new HttpClient().GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonConvert.DeserializeObject(json);

                        resultado.Add(new WatchlistConDetallesDto
                        {
                            Id = item.Id,
                            UsuarioId = item.UsuarioId,
                            ItemId = item.ItemId,
                            Tipo = item.Tipo,
                            Prioridad = item.Prioridad,
                            Title = data.title ?? data.name,
                            ImageURL = data.poster_path != null ? $"https://image.tmdb.org/t/p/w500{data.poster_path}" : null,
                            Description = data.overview,
                            ReleaseDate = data.release_date ?? data.first_air_date
                        });
                    }
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista con detalles: {ex.Message}");
            }
        }

    }
}
