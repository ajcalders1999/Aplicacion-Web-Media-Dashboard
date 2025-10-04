using System.ComponentModel.DataAnnotations;

namespace Peliculas.Abstractions.Modelos
{


    namespace Peliculas.Abstractions.Modelos
    {
        public class Favorito
        {
            public int Id { get; set; }

            [Required]
            public int UsuarioId { get; set; }

            [Required]
            public int ItemId { get; set; }

            [Required]
            [MaxLength(50)]
            public string Tipo { get; set; }

            [MaxLength(500)]
            public string Comentario { get; set; }

            [Range(0, 10)]
            public int Calificacion { get; set; }
        }
    }
}