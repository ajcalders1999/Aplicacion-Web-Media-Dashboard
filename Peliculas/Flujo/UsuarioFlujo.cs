
using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;


namespace Peliculas.Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IConfiguration _config;

        public UsuarioFlujo(IUsuarioRepositorio repositorio, IConfiguration config)
        {
            _repositorio = repositorio;
            _config = config;
        }

        public async Task<ResultadoOperacion> Registrar(Usuario usuario)
        {
            return await _repositorio.Registrar(usuario);
        }

        public async Task<ResultadoLogin> Login(LoginRequest login)
        {
            var usuario = await _repositorio.ObtenerUsuarioPorCredenciales(login.UserName, login.Password);
            if (usuario == null)
            {
                return new ResultadoLogin { Exitoso = false, Mensaje = "Credenciales incorrectas" };
            }

            return new ResultadoLogin { Exitoso = true, Usuario = usuario };
        }
    }
}
