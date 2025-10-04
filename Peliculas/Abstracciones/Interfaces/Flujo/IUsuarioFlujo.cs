using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;

namespace Peliculas.Abstractions.Interfaces.Flujo
{
    public interface IUsuarioFlujo
    {
        Task<ResultadoOperacion> Registrar(Usuario usuario);
        Task<ResultadoLogin> Login(LoginRequest login);
    }
}
