using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IUsuarioDA
    {
        Task<int> CrearUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuarioPorUsername(string username);
    }
}
