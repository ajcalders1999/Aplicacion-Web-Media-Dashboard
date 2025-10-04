using System.ComponentModel.DataAnnotations;

namespace Peliculas.Abstractions.Modelos
{


namespace Peliculas.Abstractions.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
}