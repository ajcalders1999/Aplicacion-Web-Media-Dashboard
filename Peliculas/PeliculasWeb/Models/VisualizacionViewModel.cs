namespace PeliculasWeb.Models
{
    public class VisualizacionViewModel
    {
        // Identificador único del registro de visualización
        public int Id { get; set; }

        // Id del usuario asociado a esta visualización
        public int UsuarioId { get; set; }

        // Id de la película o serie a visualizar
        public int PeliculaSerieId { get; set; }

        // Tipo de contenido: "Pelicula" o "Serie"
        public string Tipo { get; set; }

        // Prioridad para visualizar: 1 = Alta, 2 = Media, 3 = Baja
        public int Prioridad { get; set; }
    }
}
