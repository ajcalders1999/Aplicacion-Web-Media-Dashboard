using Peliculas.Abstractions.Modelos.Peliculas.Abstractions.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class FavoritoConDetalles : Favorito
    {
        public string Titulo { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double Rating { get; set; }
    }
}
