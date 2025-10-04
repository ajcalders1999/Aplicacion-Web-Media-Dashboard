namespace PeliculasWeb.Models
{
    public class FavoritoViewModel
    {
        // Identificador único del favorito
        public int Id { get; set; }

        // Id del usuario al que pertenece el favorito
        public int UsuarioId { get; set; }

        // Id del ítem (película, serie, etc.) favorito
        public int ItemId { get; set; }

        // Tipo de ítem (ejemplo: "movie", "series")
        public string Tipo { get; set; }

        // Comentario o nota asociada al favorito
        public string Comentario { get; set; }

        // Calificación otorgada al favorito
        public int Calificacion { get; set; }

        // Propiedades para mostrar información visual del ítem

        // Título del ítem (usar "Title" en vez de "Titulo")
        public string Title { get; set; }

        // Descripción del ítem
        public string Description { get; set; }

        // URL de la imagen o portada del ítem
        public string ImageURL { get; set; }

        // Fecha de lanzamiento (puede ser nula)
        public DateTime? ReleaseDate { get; set; }

        // Valoración general del ítem (por ejemplo, calificación)
        public double Rating { get; set; }
    }
}
