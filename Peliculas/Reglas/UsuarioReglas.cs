using Peliculas.Abstractions.Interfaces;
using Peliculas.Abstractions.Interfaces.Flujo;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Threading.Tasks;

namespace Reglas
{
    public class UsuarioReglas : IUsuarioReglas
    {
        private readonly IUsuarioFlujo _usuarioFlujo;

        // Constructor que recibe la implementación del flujo de usuario
        public UsuarioReglas(IUsuarioFlujo usuarioFlujo)
        {
            _usuarioFlujo = usuarioFlujo;
        }

        // Método que valida si un usuario puede ser registrado
        public async Task ValidarRegistroUsuario(Usuario usuario)
        {
            // Intenta registrar al usuario a través del flujo
            var existente = await _usuarioFlujo.Registrar(usuario);

            // Si el resultado no es null, significa que ya existe un usuario con ese nombre
            if (existente != null)
            {
                throw new InvalidOperationException("El nombre de usuario ya existe.");
            }
        }
    }
}
