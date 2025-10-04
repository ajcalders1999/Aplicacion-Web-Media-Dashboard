using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ActualizarFavoritoDto
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
    }
}
