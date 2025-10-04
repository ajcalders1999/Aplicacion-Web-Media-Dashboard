namespace PeliculasWeb.Models
{
    public class GeneroViewModel
    {
        // Identificador único del género
        public int Id { get; set; }

        // Nombre del género (debe coincidir con el JSON recibido de la API)
        public string Nombre { get; set; }
    }
}
