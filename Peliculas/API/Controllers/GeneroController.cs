
using Microsoft.AspNetCore.Mvc;
using Peliculas.Abstractions.Interfaces.Flujo;
using System;
using System.Threading.Tasks;

namespace Peliculas.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroFlujo _generoFlujo;

        public GeneroController(IGeneroFlujo generoFlujo)
        {
            _generoFlujo = generoFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerGeneros()
        {
            try
            {
                var generos = await _generoFlujo.ObtenerGeneros();
                return Ok(generos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener géneros: {ex.Message}");
            }
        }
        [HttpGet("peliculas")]
        public async Task<IActionResult> ObtenerGenerosPeliculas()
        {
            try
            {
                var generos = await _generoFlujo.ObtenerGenerosPeliculas();
                return Ok(generos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener géneros de películas: {ex.Message}");
            }
        }

        [HttpGet("series")]
        public async Task<IActionResult> ObtenerGenerosSeries()
        {
            try
            {
                var generos = await _generoFlujo.ObtenerGenerosSeries();
                return Ok(generos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener géneros de series: {ex.Message}");
            }
        }


    }
}
