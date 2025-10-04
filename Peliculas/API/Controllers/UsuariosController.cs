
using Microsoft.AspNetCore.Mvc;
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System.Threading.Tasks;

namespace Peliculas.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioFlujo _flujo;

        public UsuariosController(IUsuarioFlujo flujo)
        {
            _flujo = flujo;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El usuario es requerido.");

            var resultado = await _flujo.Registrar(usuario);
            if (!resultado.Exitoso) return BadRequest(resultado.Mensaje);
            return Ok(resultado);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest("El nombre de usuario y la contraseña son obligatorios.");

            var resultado = await _flujo.Login(login);
            if (!resultado.Exitoso) return Unauthorized(resultado.Mensaje);
            return Ok(resultado);
        }
    }
}
