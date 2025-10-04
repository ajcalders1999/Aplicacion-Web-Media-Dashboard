
using Peliculas.Abstractions.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IGeneroRepositorio
    {
        Task<IEnumerable<Genero>> ObtenerGeneros();
    }
}
