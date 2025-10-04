using Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IGeneroDA
    {
        // Obtiene la lista completa de géneros
        Task<IEnumerable<Genero>> ObtenerGeneros();
    }
}
