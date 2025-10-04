
using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;

namespace Peliculas.Abstractions.Modelos
{
    public class ResultadoLogin
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; } // Devolver los datos del usuario autenticado
    }

}
