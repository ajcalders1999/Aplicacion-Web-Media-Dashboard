
using Microsoft.AspNetCore.Mvc;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using System;
using System.Threading.Tasks;

namespace Peliculas.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaFlujo _flujo;

        public MediaController(IMediaFlujo flujo)
        {
            _flujo = flujo;
        }

        [HttpGet("peliculas/{generoId}")]
        public async Task<IActionResult> ObtenerPeliculasPorGenero(int generoId)
        {
            try
            {
                var peliculas = await _flujo.ObtenerPeliculasPorGenero(generoId);
                return Ok(peliculas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener películas: {ex.Message}");
            }
        }

        [HttpGet("series/{generoId}")]
        public async Task<IActionResult> ObtenerSeriesPorGenero(int generoId)
        {
            try
            {
                var series = await _flujo.ObtenerSeriesPorGenero(generoId);
                return Ok(series);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener series: {ex.Message}");
            }
        }
        [HttpGet("peliculas")]
        public async Task<IActionResult> ObtenerTodasLasPeliculas()
        {
            //var peliculas = await _flujo.ObtenerTodasLasPeliculas(); // Método que consulta todas las películas
            //return Ok(peliculas);
            return null;
        }
    }
}
