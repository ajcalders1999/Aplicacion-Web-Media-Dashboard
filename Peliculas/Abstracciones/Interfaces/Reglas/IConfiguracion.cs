using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peliculas.Abstractions.Interfaces
{
    public interface IConfiguracion
    {
            string ObtenerMetodo(string seccion, string nombre, int generoId);
            string ObtenerValor(string clave);

    }
}
