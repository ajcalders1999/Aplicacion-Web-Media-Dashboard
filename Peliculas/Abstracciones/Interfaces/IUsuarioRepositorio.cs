
using Peliculas.Abstractions.Modelos;
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> ObtenerUsuarioPorCredenciales(string username, string password);
        Task<ResultadoOperacion> Registrar(Usuario usuario);
    }
}
