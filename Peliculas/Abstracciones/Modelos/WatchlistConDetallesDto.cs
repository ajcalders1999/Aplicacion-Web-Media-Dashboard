using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class WatchlistConDetallesDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ItemId { get; set; }
        public string Tipo { get; set; }
        public int Prioridad { get; set; }

        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
    }

}
