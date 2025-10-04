
namespace Peliculas.Abstractions.Modelos
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string MediaType { get; set; }
    }
}
