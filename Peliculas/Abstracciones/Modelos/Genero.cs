
using Newtonsoft.Json;

namespace Peliculas.Abstractions.Modelos
{
    public class Genero
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Nombre { get; set; }  // ← se llena correctamente con "name" desde TMDB
    }
}
