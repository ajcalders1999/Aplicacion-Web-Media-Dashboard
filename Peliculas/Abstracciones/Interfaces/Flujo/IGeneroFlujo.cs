
using Peliculas.Abstractions.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces.Flujo
{
    public interface IGeneroFlujo
    {
        public Task<IEnumerable<Genero>> ObtenerGeneros();
        public Task<IEnumerable<Genero>> ObtenerGenerosPeliculas();
        public Task<IEnumerable<Genero>> ObtenerGenerosSeries();

    }
}
