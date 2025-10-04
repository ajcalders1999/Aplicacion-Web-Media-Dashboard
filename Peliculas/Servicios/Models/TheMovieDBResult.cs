using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Peliculas.Servicios
{
    public class TheMovieDBResult
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public List<TheMovieDBItem> Results { get; set; }

        // Opcional: otras propiedades del JSON, como:
        // total_pages, total_results, etc.
    }

    public class TheMovieDBItem
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }
    }
    public class TheMovieDBGenerosResponse
    {
        [JsonPropertyName("genres")]
        public List<GeneroDto> Genres { get; set; }
    }

    public class GeneroDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}
