namespace PeliculasWeb.Models
{
    public class PeliculaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ImagenUrl { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaLanzamiento { get; set; }
        public double Calificacion { get; set; }
    }
}
