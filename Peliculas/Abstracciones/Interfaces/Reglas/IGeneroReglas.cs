using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IGeneroReglas
    {
        Task ValidarGeneroExistente(int generoId);
    }
}
