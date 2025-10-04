
using Microsoft.AspNetCore.Mvc;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Modelos;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using Abstracciones.Modelos;

namespace Peliculas.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        private readonly IFavoritoFlujo _flujo;

        public FavoritosController(IFavoritoFlujo flujo)
        {
            _flujo = flujo;
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<FavoritoConDetalles>>> ObtenerFavoritosPorUsuario(int usuarioId)
        {
            var favoritos = await _flujo.ObtenerFavoritosConDetalles(usuarioId);
            return Ok(favoritos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarFavorito([FromBody] Favorito favorito)
        {
            if (favorito == null)
                return BadRequest("El favorito es requerido.");

            await _flujo.AgregarFavorito(favorito);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarFavorito(int id)
        {
            await _flujo.EliminarFavorito(id);
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> ActualizarFavorito([FromBody] ActualizarFavoritoDto favorito)
        {
            if (favorito == null)
                return BadRequest("El favorito es requerido.");

            var resultado = await _flujo.ActualizarFavorito(favorito);

            if (!resultado)
                return BadRequest("No se pudo actualizar el favorito.");

            return Ok("Favorito actualizado correctamente");
        }


    }
}
