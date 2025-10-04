namespace PeliculasWeb.Models
{
    public class HomeViewModel
    {
        // Lista de géneros disponibles para mostrar en la vista
        public List<GeneroViewModel> Generos { get; set; }

        // Lista de películas que se mostrarán (filtradas por género)
        public List<MediaViewModel> Peliculas { get; set; }

        // Id del género actualmente seleccionado (puede ser null)
        public int? GeneroSeleccionado { get; set; }

        // Lista de IDs de ítems favoritos del usuario
        public List<int> FavoritosIds { get; set; } = new();

        // Lista de series que se mostrarán en la vista
        public List<MediaViewModel> Series { get; set; } = new();

        // Lista de IDs de películas en la watchlist del usuario
        public List<int> WatchlistIds { get; set; } = new();
    }
}
