using System.ComponentModel.DataAnnotations;

namespace Peliculas.Abstractions.Modelos
{


namespace Peliculas.Abstractions.Modelos
{
    public class Watchlist
    {
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; }

        [Range(1, 5)]
        public int Prioridad { get; set; }
    }
}
}