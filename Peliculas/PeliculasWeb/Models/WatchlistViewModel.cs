namespace PeliculasWeb.Models
{
    public class WatchlistViewModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ItemId { get; set; }
        public string Tipo { get; set; } // "Pelicula" o "Serie"
        public int Prioridad { get; set; } // "Alta", "Media", "Baja"
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }

    }

}
